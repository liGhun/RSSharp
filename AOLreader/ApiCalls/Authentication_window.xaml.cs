using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Reflection;

namespace RSSharp.AOLreader.ApiCalls
{
    /// <summary>
    /// Interaction logic for Authentication_window.xaml
    /// </summary>
    public partial class Authentication_window : Window
    {
 
        private string state { get; set; }

        private bool complete { get; set; }

        private bool initialLogoutCompleted;
        private string client_id { get; set; }
        private string client_secret { get; set; }
        private string redirect_uri { get; set; }

        public string authUrl { get; private set; }
        public System.Windows.Forms.WebBrowser webBrowserAuthorization;

        public Authentication_window(string clientId, string clientSecret, string redirectUrl, string scope = "https://api.screenname.aol.com/auth/authorize ", string response_type = "code", bool logOutFirst = false, string state = null)
        {
            InitializeComponent();
            initialLogoutCompleted = !logOutFirst;
            Guid guid = System.Guid.NewGuid();
            state = guid.ToString();
            client_id = clientId;
            client_secret = clientSecret;
            redirect_uri = redirectUrl;

            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();
            webBrowserAuthorization = new System.Windows.Forms.WebBrowser();
            host.Child = webBrowserAuthorization;

            this.mainGrid.Children.Add(host);

            webBrowserAuthorization.Navigated += webBrowserAuthorization_Navigated;
            webBrowserAuthorization.Navigating += webBrowserAuthorization_Navigating;
            authUrl = Authentications.get_authentication_url(response_type: response_type, client_id: clientId, redirect_uri: redirectUrl, scope: scope, state: state);
            
        }

        void webBrowserAuthorization_Navigating(object sender, System.Windows.Forms.WebBrowserNavigatingEventArgs e)
        {
            return;
            if (!initialLogoutCompleted)
            {
                return;
            }
            if (e != null)
            {
                if (e.Url.AbsoluteUri.Contains("code="))
                {
                    complete = true;

                    Model.Authentication.auth_response response = Authentications.parse_authentication_reponse(e.Url.AbsoluteUri);
                    AuthEventArgs eventArgs = new AuthEventArgs();
                    if (response.success)
                    {
                        Model.Authentication.token token = Authentications.get_access_token(response.code, client_id, client_secret, redirect_uri);
                        if (token != null)
                        {
                            eventArgs.success = true;
                            eventArgs.token = token;
                        }
                        else
                        {
                            eventArgs.error = "Unable to retrieve access token from code";
                        }
                    }
                    else
                    {
                        eventArgs.error = response.error_message;
                    }
                    AuthSuccess(this, eventArgs);
                    Close();
                }


            }
        }

        public void startAuthorization()
        {
            if (!initialLogoutCompleted)
            {
                webBrowserAuthorization.Navigate("https://alpha.app.net/logout/");
            }
            else
            {
                webBrowserAuthorization.Navigate(authUrl);
            }
        }

        void webBrowserAuthorization_Navigated(object sender, System.Windows.Forms.WebBrowserNavigatedEventArgs e)
        {
         
            //HideScriptErrors(webBrowserAuthorization, true);
        
            if (!initialLogoutCompleted)
            {
                initialLogoutCompleted = true;
                startAuthorization();
                return;
            }
            else
            {
                if (e != null)
                {
                    if (e.Url.AbsoluteUri.Contains("code="))
                    {
                        complete = true;

                        Model.Authentication.auth_response response = Authentications.parse_authentication_reponse(e.Url.AbsoluteUri);
                        AuthEventArgs eventArgs = new AuthEventArgs();
                        if (response.success)
                        {
                            Model.Authentication.token token = Authentications.get_access_token(response.code, client_id, client_secret,redirect_uri);
                            if (token != null)
                            {
                                eventArgs.success = true;
                                eventArgs.token = token;
                            }
                            else
                            {
                                eventArgs.error = "Unable to retrieve access token from code";
                            }
                        }
                        else
                        {
                            eventArgs.error = response.error_message;
                        }
                        AuthSuccess(this, eventArgs);
                        Close();
                    }

                    
                }
            }
        }


        public void HideScriptErrors(WebBrowser wb, bool Hide)
        {
            FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;

            object objComWebBrowser = fiComWebBrowser.GetValue(wb);

            if (objComWebBrowser == null) return;

            objComWebBrowser.GetType().InvokeMember(

            "Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { Hide });

        }

        
        void webBrowserAuthorization_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            if (e != null)
            {
                WebBrowser browser = sender as WebBrowser;
                MessageBox.Show(e.Source.ToString());
            }
        }




        
        public event AuthEventHandler AuthSuccess;
        public delegate void AuthEventHandler(object sender, AuthEventArgs e);
        public class AuthEventArgs : EventArgs
        {

            public bool success { get; set; }
            public Model.Authentication.token token {get; set;}
            public string error { get; set; }
        }


         public static class WebBrowserHelper
       {
           #region Definitions/DLL Imports
          /// <summary>
          /// For PInvoke: Contains information about an entry in the Internet cache
         /// </summary>
         [StructLayout(LayoutKind.Explicit, Size = 80)]
         public struct INTERNET_CACHE_ENTRY_INFOA
         {
             [FieldOffset(0)]
             public uint dwStructSize;
             [FieldOffset(4)]
             public IntPtr lpszSourceUrlName;
             [FieldOffset(8)]
             public IntPtr lpszLocalFileName;
             [FieldOffset(12)]
             public uint CacheEntryType;
             [FieldOffset(16)]
             public uint dwUseCount;
             [FieldOffset(20)]
             public uint dwHitRate;
             [FieldOffset(24)]
             public uint dwSizeLow;
             [FieldOffset(28)]
             public uint dwSizeHigh;
             [FieldOffset(32)]
             public System.Runtime.InteropServices.ComTypes.FILETIME LastModifiedTime;
             [FieldOffset(40)]
             public System.Runtime.InteropServices.ComTypes.FILETIME ExpireTime;
             [FieldOffset(48)]
             public System.Runtime.InteropServices.ComTypes.FILETIME LastAccessTime;
             [FieldOffset(56)]
             public System.Runtime.InteropServices.ComTypes.FILETIME LastSyncTime;
             [FieldOffset(64)]
             public IntPtr lpHeaderInfo;
             [FieldOffset(68)]
             public uint dwHeaderInfoSize;
             [FieldOffset(72)]
             public IntPtr lpszFileExtension;
             [FieldOffset(76)]
             public uint dwReserved;
             [FieldOffset(76)]
             public uint dwExemptDelta;
         }
  
         // For PInvoke: Initiates the enumeration of the cache groups in the Internet cache
         [DllImport(@"wininet",
             SetLastError = true,
             CharSet = CharSet.Auto,
             EntryPoint = "FindFirstUrlCacheGroup",
             CallingConvention = CallingConvention.StdCall)]
         public static extern IntPtr FindFirstUrlCacheGroup(
             int dwFlags,
             int dwFilter,
             IntPtr lpSearchCondition,
             int dwSearchCondition,
             ref long lpGroupId,
             IntPtr lpReserved);
  
         // For PInvoke: Retrieves the next cache group in a cache group enumeration
         [DllImport(@"wininet",
             SetLastError = true,
             CharSet = CharSet.Auto,
             EntryPoint = "FindNextUrlCacheGroup",
             CallingConvention = CallingConvention.StdCall)]
         public static extern bool FindNextUrlCacheGroup(
             IntPtr hFind,
             ref long lpGroupId,
             IntPtr lpReserved);
  
         // For PInvoke: Releases the specified GROUPID and any associated state in the cache index file
         [DllImport(@"wininet",
             SetLastError = true,
             CharSet = CharSet.Auto,
             EntryPoint = "DeleteUrlCacheGroup",
             CallingConvention = CallingConvention.StdCall)]
         public static extern bool DeleteUrlCacheGroup(
             long GroupId,
             int dwFlags,
             IntPtr lpReserved);
  
         // For PInvoke: Begins the enumeration of the Internet cache
         [DllImport(@"wininet",
             SetLastError = true,
             CharSet = CharSet.Auto,
             EntryPoint = "FindFirstUrlCacheEntryA",
             CallingConvention = CallingConvention.StdCall)]
         public static extern IntPtr FindFirstUrlCacheEntry(
             [MarshalAs(UnmanagedType.LPTStr)] string lpszUrlSearchPattern,
             IntPtr lpFirstCacheEntryInfo,
             ref int lpdwFirstCacheEntryInfoBufferSize);
  
         // For PInvoke: Retrieves the next entry in the Internet cache
         [DllImport(@"wininet",
             SetLastError = true,
             CharSet = CharSet.Auto,
             EntryPoint = "FindNextUrlCacheEntryA",
             CallingConvention = CallingConvention.StdCall)]
         public static extern bool FindNextUrlCacheEntry(
             IntPtr hFind,
             IntPtr lpNextCacheEntryInfo,
             ref int lpdwNextCacheEntryInfoBufferSize);
  
         // For PInvoke: Removes the file that is associated with the source name from the cache, if the file exists
         [DllImport(@"wininet",
             SetLastError = true,
             CharSet = CharSet.Auto,
             EntryPoint = "DeleteUrlCacheEntryA",
             CallingConvention = CallingConvention.StdCall)]
         public static extern bool DeleteUrlCacheEntry(
             IntPtr lpszUrlName);
         #endregion
  
         #region Public Static Functions
  
         /// <summary>
         /// Clears the cache of the web browser
         /// </summary>
         public static void ClearCache()
         {
             // Indicates that all of the cache groups in the user's system should be enumerated
             const int CACHEGROUP_SEARCH_ALL = 0x0;
             // Indicates that all the cache entries that are associated with the cache group
             // should be deleted, unless the entry belongs to another cache group.
             const int CACHEGROUP_FLAG_FLUSHURL_ONDELETE = 0x2;
             // File not found.
             const int ERROR_FILE_NOT_FOUND = 0x2;
             // No more items have been found.
             const int ERROR_NO_MORE_ITEMS = 259;
             // Pointer to a GROUPID variable
             long groupId = 0;
  
             // Local variables
             int cacheEntryInfoBufferSizeInitial = 0;
             int cacheEntryInfoBufferSize = 0;
             IntPtr cacheEntryInfoBuffer = IntPtr.Zero;
             INTERNET_CACHE_ENTRY_INFOA internetCacheEntry;
             IntPtr enumHandle = IntPtr.Zero;
             bool returnValue = false;
  
             // Delete the groups first.
             // Groups may not always exist on the system.
             // For more information, visit the following Microsoft Web site:
             // http://msdn.microsoft.com/library/?url=/workshop/networking/wininet/overview/cache.asp            
             // By default, a URL does not belong to any group. Therefore, that cache may become
             // empty even when the CacheGroup APIs are not used because the existing URL does not belong to any group.            
             enumHandle = FindFirstUrlCacheGroup(0, CACHEGROUP_SEARCH_ALL, IntPtr.Zero, 0, ref groupId, IntPtr.Zero);
             // If there are no items in the Cache, you are finished.
             if (enumHandle != IntPtr.Zero && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
                 return;
  
             // Loop through Cache Group, and then delete entries.
             while (true)
             {
                 if (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error() || ERROR_FILE_NOT_FOUND == Marshal.GetLastWin32Error()) { break; }
                 // Delete a particular Cache Group.
                 returnValue = DeleteUrlCacheGroup(groupId, CACHEGROUP_FLAG_FLUSHURL_ONDELETE, IntPtr.Zero);
                 if (!returnValue && ERROR_FILE_NOT_FOUND == Marshal.GetLastWin32Error())
                 {
                     returnValue = FindNextUrlCacheGroup(enumHandle, ref groupId, IntPtr.Zero);
                 }
  
                 if (!returnValue && (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error() || ERROR_FILE_NOT_FOUND == Marshal.GetLastWin32Error()))
                     break;
             }
  
             // Start to delete URLs that do not belong to any group.
             enumHandle = FindFirstUrlCacheEntry(null, IntPtr.Zero, ref cacheEntryInfoBufferSizeInitial);
             if (enumHandle != IntPtr.Zero && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
                 return;
  
             cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
             cacheEntryInfoBuffer = Marshal.AllocHGlobal(cacheEntryInfoBufferSize);
             enumHandle = FindFirstUrlCacheEntry(null, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
  
             while (true)
             {
                 internetCacheEntry = (INTERNET_CACHE_ENTRY_INFOA)Marshal.PtrToStructure(cacheEntryInfoBuffer, typeof(INTERNET_CACHE_ENTRY_INFOA));
                 if (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error()) { break; }
  
                 cacheEntryInfoBufferSizeInitial = cacheEntryInfoBufferSize;
                 returnValue = DeleteUrlCacheEntry(internetCacheEntry.lpszSourceUrlName);
                 if (!returnValue)
                 {
                     returnValue = FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                 }
                 if (!returnValue && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
                 {
                     break;
                 }
                 if (!returnValue && cacheEntryInfoBufferSizeInitial > cacheEntryInfoBufferSize)
                 {
                     cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
                     cacheEntryInfoBuffer = Marshal.ReAllocHGlobal(cacheEntryInfoBuffer, (IntPtr)cacheEntryInfoBufferSize);
                     returnValue = FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                 }
             }
             Marshal.FreeHGlobal(cacheEntryInfoBuffer);
         }
         #endregion
     }
    }
}
