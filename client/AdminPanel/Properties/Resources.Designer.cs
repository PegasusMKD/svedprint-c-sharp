﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminPanel.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AdminPanel.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to login/.
        /// </summary>
        public static string LoginRoute {
            get {
                return ResourceManager.GetString("LoginRoute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to return_users/.
        /// </summary>
        public static string RetrieveUsersRoute {
            get {
                return ResourceManager.GetString("RetrieveUsersRoute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://127.0.0.1:8000/admin_stable/.
        /// </summary>
        public static string ServerURI {
            get {
                return ResourceManager.GetString("ServerURI", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to update_admin/.
        /// </summary>
        public static string UpdateAdminRoute {
            get {
                return ResourceManager.GetString("UpdateAdminRoute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to update_users/.
        /// </summary>
        public static string UpdateUsersRoute {
            get {
                return ResourceManager.GetString("UpdateUsersRoute", resourceCulture);
            }
        }
    }
}
