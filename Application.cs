using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Clish.Commands;
using Clish.Library;
using Clish.Library.Models;
using Clish.Logs.Logs;
using Clish.Terminal;
using Mono.Options;
using Mono.Terminal;
using Completion = Mono.Terminal.LineEditor.Completion;
using HandleKeyEventArgs = Mono.Terminal.LineEditor.HandleKeyEventArgs;

namespace Clish
{
    ///<summary>
    /// Main part of application.
    ///</summary>
    public class Application
    {
        #region [  Fields  ]

        private Thread m_thread;

        private event ConfigurationLoadedHandler OnConfigurationLoaded;

        #endregion

        #region [  Constructors  ]

        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// </summary>
        public Application()
        {
            OnConfigurationLoaded += OnApplicationConfigurationLoaded;
        }

        #endregion

        #region [  Properties  ]

        /// <summary>
        /// Current session of the application by default is empty name view.
        /// </summary>
        public Session CurrentSession { get; set; }

        /// <summary>
        /// Contains all commands, modules and types of loaded xml files.
        /// </summary>
        public Configuration Configuration { get; set; }

        #endregion

        #region [  Creation of commands  ]

        // Create internal clish commands.
        private void CreateInternalCommands()
        {
            if (!Configuration.Views.ContainsKey(Configuration.DefaultViewName))
            {
                Configuration.Views.Add(Configuration.DefaultViewName, new CommandNode());
            }
            // we have to change it to another to logout command
            Configuration.Views[Configuration.DefaultViewName].Add(new LogoutCommand(this));
            Configuration.Views[Configuration.DefaultViewName].Add(new ExitCommand(this));
            Configuration.Views[Configuration.DefaultViewName].Add(new TopCommand(this));
        }

        #endregion

        /// <summary>
        /// Entry point to clish application.
        /// </summary>
        /// <param name="args">The args.</param>
        public void Run(String[] args)
        {
            OptionSet optionSet = CreateOptionSet();
            try
            {
                Log.CoreLog.Info("Starting the application and parsing command line arguments.");
                List<string> parsed = optionSet.Parse(args);

                //  If we not found any commands in arguments we need 
                // to work in terminal emulation.
                if (parsed.Count == 0)
                {
                    Log.CoreLog.Info("Create main thread for terminal manipulations.");
                    m_thread = new Thread(Loop);
                    m_thread.Start(); // Check it
                    Log.CoreLog.Info("Waiting while loading configuration settings.");
                    m_thread.Join();
                }
            }
            catch (Exception ex)
            {
                Log.CoreLog.Error(ex);
            }
        }

        /// <summary>
        /// Method implements main application loop where application search configuration files, load them and
        /// then run LineEditor (emulator of Unix line terminal).
        /// </summary>
        private void Loop()
        {
            PreloadConfiguration();

            String command;
            var editor = new LineEditor(null);
            editor.AutoCompleteEvent += OnAutoComplete;
            editor.TabKeyEvent += OnEditorTabKeyEvent;
            while ((command = editor.Edit(CurrentSession.Prompt, "")) != null)
            {
                RunCommand(command);
            }
        }

        private void OnEditorTabKeyEvent(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty((e as HandleKeyEventArgs).Text))
            {
                CurrentSession.ShowAvailableCommands().ForEach(Console.WriteLine);
                (sender as LineEditor).Render();
            }
        }

        /// <summary>
        /// Creates the option set for parsing command line params.
        /// INFO: At the current moment we have only help command for showing
        /// by user request in the command line.
        /// </summary>
        /// <returns></returns>
        private static OptionSet CreateOptionSet()
        {
            OptionSet optionSet = new OptionSet()
                .Add("?|h|help",
                     @"
    Display context sensitive help. This is either a list of possible
    command completions with summaries, or the full syntax of the
    current command. A subsequent repeat of this key, when a command
    has been resolved, will display a detailed reference.",
                     option => ShowHelp());
            return optionSet;
        }

        private static void ShowHelp()
        {
            Log.CoreLog.Info("Show help information to user. Help contains general information about configuration.");
            Console.WriteLine(Constants.HelpTemplate);
        }

        /// <summary>
        /// Method implements loading configuration from the CLISH_PATH environement key.
        /// Configuration class parse defined path and then try deserialize xml config files to
        /// application model objects.
        /// </summary>
        private void PreloadConfiguration()
        {
            // Try to get clish path for preloading commands or another settings.
            String path = Environment.GetEnvironmentVariable(Constants.ClishPath);
            if (String.IsNullOrEmpty(path))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), Constants.DefaultConfigurationDirectory);
            }
            Log.CoreLog.Info(
                String.Format("Get configuration settings path for loading commands from key: {0} and value: {1}",
                              Constants.ClishPath, path));
            if (!String.IsNullOrEmpty(path))
            {
                Log.CoreLog.Info("Read all files from configuration directory and create prototypes for it.");
                Configuration = new Configuration(path);
            }

            InvokeOnConfigurationLoaded(EventArgs.Empty);
        }

        /// <summary>
        /// Invokes the on configuration loaded.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void InvokeOnConfigurationLoaded(EventArgs e)
        {
            ConfigurationLoadedHandler loaded = OnConfigurationLoaded;
            if (loaded != null)
            {
                loaded(this, e);
            }
        }

        /// <summary>
        /// Called when [application configuration loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnApplicationConfigurationLoaded(object sender, EventArgs e)
        {
            // Create session with default prompt and view with default view name.
            CurrentSession = new Session(Configuration, Configuration.DefaultViewName);
            // Create clish internal commands.
            CreateInternalCommands();
            // Show startup and run defined action in the startup if we have it.
            ShowStartup();
        }

        /// <summary>
        /// Shows the startup for in the first loading of application.
        /// If in the configuration startup administator created rule for startup 
        /// we can try to run it.
        /// </summary>
        private void ShowStartup()
        {
            // Show only global startup on the session starting.
            Startup startup =
                (from c in Configuration.Modules where c.Startup != null select c.Startup).FirstOrDefault();
            if (startup != null)
            {
                Console.WriteLine(startup.Detail.TrimStart());
                RunCommand(startup.Action.Text);
            }
        }

        /// <summary>
        /// Run raw command using SysCall and show results in the terminal.
        /// </summary>
        /// <param name="command">The command.</param>
        private void RunCommand(String command)
        {
            // Syscall.run(command); INFO: Read results and print to terminal.
            Console.WriteLine(command);
        }

        /// <summary>
        /// Called when auto complete is running. That's fired by LineEditor.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        private Completion OnAutoComplete(String line, int position)
        {
            // TODO: We have to replace Search method with searching by any keychars.
            var completion = new Completion(String.Empty, new string[0]);
            var node = CurrentSession.CommandNode.Search(line);
            if (node != null)
            {
                List<String> content = new List<String>();
                node.ForEach(p => content.AddRange(p.Keys));
                completion = new Completion(String.Empty, content.ToArray());
            }
            return completion;
        }

        #region Nested type: ConfigurationLoadedHandler

        private delegate void ConfigurationLoadedHandler(object sender, EventArgs e);

        #endregion
    }
}