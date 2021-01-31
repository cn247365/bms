namespace WebApp.Models.resource {
  using System;
  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public class Stock {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Stock() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApp.Models.resource.Stock", typeof(Stock).Assembly);
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
    public static string Id {
            get {
                return ResourceManager.GetString("Id", resourceCulture);
            }
    }
    public static string BookId {
            get {
                return ResourceManager.GetString("BookId", resourceCulture);
            }
    }
    public static string BarCode {
            get {
                return ResourceManager.GetString("BarCode", resourceCulture);
            }
    }
    public static string ISBN {
            get {
                return ResourceManager.GetString("ISBN", resourceCulture);
            }
    }
    public static string Title {
            get {
                return ResourceManager.GetString("Title", resourceCulture);
            }
    }
    public static string Remark {
            get {
                return ResourceManager.GetString("Remark", resourceCulture);
            }
    }
    public static string Qty {
            get {
                return ResourceManager.GetString("Qty", resourceCulture);
            }
    }
    public static string Price {
            get {
                return ResourceManager.GetString("Price", resourceCulture);
            }
    }
    public static string PurchasingPrice {
            get {
                return ResourceManager.GetString("PurchasingPrice", resourceCulture);
            }
    }
    public static string PurchaseDate {
            get {
                return ResourceManager.GetString("PurchaseDate", resourceCulture);
            }
    }
    public static string UserName {
            get {
                return ResourceManager.GetString("UserName", resourceCulture);
            }
    }
    public static string Catetory {
            get {
                return ResourceManager.GetString("Catetory", resourceCulture);
            }
    }
    public static string Location {
            get {
                return ResourceManager.GetString("Location", resourceCulture);
            }
    }
    public static string Shelves {
            get {
                return ResourceManager.GetString("Shelves", resourceCulture);
            }
    }
    public static string Tags {
            get {
                return ResourceManager.GetString("Tags", resourceCulture);
            }
    }
    public static string Status {
            get {
                return ResourceManager.GetString("Status", resourceCulture);
            }
    }
    public static string Trades {
            get {
                return ResourceManager.GetString("Trades", resourceCulture);
            }
    }
    public static string InputDate {
            get {
                return ResourceManager.GetString("InputDate", resourceCulture);
            }
    }
 }
}