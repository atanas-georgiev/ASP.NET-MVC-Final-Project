﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Planex.Services.Messages {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class SystemMessagesResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SystemMessagesResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Planex.Services.Messages.SystemMessagesResources", typeof(SystemMessagesResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Estimation approved for project {0}.
        /// </summary>
        public static string ProjectApprovedSubject {
            get {
                return ResourceManager.GetString("ProjectApprovedSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello {0}, your estimation was approved by manager {1}. Project name is {2}. The project is not started..
        /// </summary>
        public static string ProjectApprovedText {
            get {
                return ResourceManager.GetString("ProjectApprovedText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Project {0} completed..
        /// </summary>
        public static string ProjectCompletedSubject {
            get {
                return ResourceManager.GetString("ProjectCompletedSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello {0}, project {1} was completed. Good Job!.
        /// </summary>
        public static string ProjectCompletedText {
            get {
                return ResourceManager.GetString("ProjectCompletedText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Estimation completed for project {0}.
        /// </summary>
        public static string ProjectEstimatedSubject {
            get {
                return ResourceManager.GetString("ProjectEstimatedSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello {0}, new estimation task was completed by lead {1}. Project name is {2}. You can check the provided estimation details in the project page..
        /// </summary>
        public static string ProjectEstimatedText {
            get {
                return ResourceManager.GetString("ProjectEstimatedText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Estimation requested for project {0}..
        /// </summary>
        public static string ProjectRequestedEstimationSubject {
            get {
                return ResourceManager.GetString("ProjectRequestedEstimationSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello {0}, new estimation was requested by manager {1}. Project name is {2}. You can check you estimation list for more details..
        /// </summary>
        public static string ProjectRequestedEstimationText {
            get {
                return ResourceManager.GetString("ProjectRequestedEstimationText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Task {0} completed..
        /// </summary>
        public static string TaskCompletedSubject {
            get {
                return ResourceManager.GetString("TaskCompletedSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello {0}, task {1} from project {2} was completed by {3}..
        /// </summary>
        public static string TaskCompletedText {
            get {
                return ResourceManager.GetString("TaskCompletedText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Task {0} overdue!.
        /// </summary>
        public static string TaskOverdueSubject {
            get {
                return ResourceManager.GetString("TaskOverdueSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello {0}, task {1} was not completed in time. Please check the status..
        /// </summary>
        public static string TaskOverdueText {
            get {
                return ResourceManager.GetString("TaskOverdueText", resourceCulture);
            }
        }
    }
}
