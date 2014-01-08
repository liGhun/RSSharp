RSSharp
=======

A .NET library giving access to various RSS feed aggregation services

# General #

All access is done using the methods in the class ApiCalls (for example
in the namepace RSSharp.Feedly.ApiCalls for Feedly) and all the data
structures can be found in the Model class (for example in the namepace
RSSharp.Feedly.Model for Feedly)

So it might be a good idea to start you files with something like

`using RSSharp.Feedly.Model;`

`using RSSharp.Feedly.ApiCalls;`

Newtonsoft's JSON library is needed and used.

# Authentication #

There is a class called "Authentications" in the ApiCalls namespace
which provides quite some help on the auth stuff. For Feedly those are:

`get_authentication_url(string response_type = "code", string client_id
= "", string redirect_uri = "", string scope
="https://cloud.feedly.com/subscriptions", string state = "")`

which provides the url you need to open in a WebBrowser for the user to
enable access to Feedly

`Authentication.auth_response parse_authentication_reponse(string url)`

A response URL parser which looks for success / error in the url

And two methods to get the access token (either directly or using a
refresh token.

As this stuff with OAuth might be quite confusing when bulding a desktop
app (it's much easier within a web app) I also added a fully automated
process which opens an extra window and does all the stuff
automatically. This is true for a Windows Desktop app but not a a Modern
UI one I have to say.

Here is how it works by the example in Menere:

		public void add_new_account()
        {
            //RSSharp.Feedly.Configuration.activate_sandbox();
            RSSharp.Feedly.ApiCalls.Authentication_window auth_window = new RSSharp.Feedly.ApiCalls.Authentication_window("*********", "**********", "http://yourRedirectUri");
            auth_window.AuthSuccess += auth_window_AuthSuccess;
            auth_window.Show();
            auth_window.startAuthorization();
        }

        void auth_window_AuthSuccess(object sender, RSSharp.Feedly.ApiCalls.Authentication_window.AuthEventArgs e)
        {
            if (e.success)
            {
                token = e.token;
            	// check token, save it, go on, ...
            }
            else
            {
                System.Windows.MessageBox.Show(e.error, "Error adding
Feedly account");
            }
        }

As you see you'll open a window and subscribe to the event "AuthSuccess"
there. In this event you get either the token or an error message as
response.

# Getting data #

Every Api endpoint has a method in the ApiCalls namespace - some examples:

## Getting the user profile ##

`Profile user_profile = Profiles.get(YourAccessToken);`

## Getting the subscribed feeds ##

`List<Subscription> subscriptions = Subscriptions.get(YourAccessToken);`

## Getting entries in a stream (in this example the saved items) ##

`Streams.entries_list saved_entries =
Streams.get_entries_in_stream(YourAccessToken,
string.Format("user/{0}/tag/global.saved", this.profile.id), count: 1000);`

# Changing items #
## Marking as read ##

`RSSharp.Feedly.ApiCalls.Markers.mark_entry_as_read(YourAccessToken,
item_id);`

## Help ##

Feel free to contact me (sven@li-ghun.de) if you have any questions
