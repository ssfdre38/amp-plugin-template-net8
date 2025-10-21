using ModuleShared;
using System.Reflection;

namespace $safeprojectname$
{
    /// <summary>
    /// Main plugin class for AMP
    /// This class will be instantiated by AMP when the plugin is loaded
    /// </summary>
    public class PluginMain : AMPPlugin
    {
        #region Plugin Metadata
        
        /// <summary>
        /// Display name shown in AMP interface
        /// </summary>
        public override string DisplayName => "$projectname$";

        /// <summary>
        /// Author name
        /// </summary>
        public override string Author => "$author$";

        /// <summary>
        /// Plugin description
        /// </summary>
        public override string Description => "Description of your plugin";

        /// <summary>
        /// Unique plugin identifier (generate a new GUID for your plugin)
        /// </summary>
        public override Guid PluginID => new Guid("00000000-0000-0000-0000-000000000000");

        /// <summary>
        /// Plugin version
        /// </summary>
        public override string PluginVersion => "1.0.0";

        /// <summary>
        /// Indicates if this plugin has a WebRoot directory with web interface files
        /// </summary>
        public override bool HasWebRoot => true;

        /// <summary>
        /// Path to WebRoot directory relative to plugin directory
        /// </summary>
        public override string WebRootPath => "./WebRoot";

        #endregion

        #region Plugin Lifecycle

        /// <summary>
        /// Called when the plugin is initialized
        /// </summary>
        /// <param name="app">AMP application wrapper</param>
        /// <param name="manifest">Plugin manifest</param>
        public override void Init(IApplicationWrapper app, ProviderManifest manifest)
        {
            base.Init(app, manifest);
            
            // Your plugin initialization code here
            Console.WriteLine($"[$safeprojectname$] Plugin initialized successfully");
            
            // Register event handlers, set up timers, etc.
        }

        /// <summary>
        /// Called when the plugin is being shut down
        /// </summary>
        public override void Shutdown()
        {
            // Clean up resources here
            Console.WriteLine($"[$safeprojectname$] Plugin shutting down");
            
            base.Shutdown();
        }

        #endregion

        #region IPC Methods

        /// <summary>
        /// Returns list of IPC methods exposed by this plugin
        /// </summary>
        public override IEnumerable<MethodInfo> GetIPCMethods()
        {
            // Return methods that should be accessible via IPC
            // Leave empty if no IPC methods needed
            return new List<MethodInfo>();
        }

        #endregion

        #region Web API Methods

        /// <summary>
        /// Example web API method
        /// This will be accessible at /API/$safeprojectname$/ExampleMethod
        /// </summary>
        /// <returns>Action result with data</returns>
        [WebMethod("Example web method", "example")]
        public ActionResult ExampleMethod()
        {
            try
            {
                return new ActionResult
                {
                    Status = ActionStatus.OK,
                    Result = new
                    {
                        success = true,
                        message = "Hello from $safeprojectname$!",
                        timestamp = DateTime.UtcNow
                    }
                };
            }
            catch (Exception ex)
            {
                return new ActionResult
                {
                    Status = ActionStatus.Failure,
                    Reason = ex.Message
                };
            }
        }

        /// <summary>
        /// Example method with parameters
        /// </summary>
        /// <param name="inputValue">Example parameter</param>
        /// <returns>Action result</returns>
        [WebMethod("Example method with parameter", "exampleWithParam")]
        public ActionResult ExampleWithParameter(string inputValue)
        {
            try
            {
                return new ActionResult
                {
                    Status = ActionStatus.OK,
                    Result = new
                    {
                        success = true,
                        receivedValue = inputValue,
                        processedValue = inputValue?.ToUpper()
                    }
                };
            }
            catch (Exception ex)
            {
                return new ActionResult
                {
                    Status = ActionStatus.Failure,
                    Reason = ex.Message
                };
            }
        }

        #endregion

        #region Helper Methods

        // Add your helper methods here

        #endregion
    }
}
