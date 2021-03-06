//---> Credential Locker sample

// Use Credential Locker to securely store credentials and 
// roam across the user's trusted devices

void SaveCredential(string username, string password)
{
  PasswordVault vault= new PasswordVault();
  PasswordCredential cred= new PassCredential("MyAppResource",username,password);
  vault.Add(cred);
}

IReadOnlyList<PasswordCredential> RetrieveCredential(string resource)
{
  PasswordVault vault= new PassVault();
  return vault.FindAllByResource(resource);
}


//---> Web Authentication Broker (WAB)

// Many apps connect to popular online services
// Authentication is usually required
// Easy authentication against online Service Providers
// e.g Facebook, Twitter, etc.

// Launching WebAuthenticationBroker
// Usage Differs On Windows and Windows Phone


//---> Initiate authentication using WAB

void Authenticate()
{
  WebAuthenticationBroker.AuthenticateAndContinue(startUri, endUri);

}
// App suspended, may be terminated
                ||
// App activated when WAB returns

{
  // Code runs on reactivation to handle response from WAB
}



//---> Activation after WebAuthenticationBroker

// This must be coded in App.cs
protected override async void OnActivated(IActivatedEventArgs args)
{
  if(args is WebAuthenticationBrokerContinuationEventArgs)
  {
    Frame rootFrame = Window.Current.Content as Frame;

    // Do standart logic to create the Frame if necessary and restore state
    if(rootFrame == null)
    {
      rootFrame = new Frame();
      SuspensionManager.RegisterFrame(rootFrame, "AppFrame");
      if (args.previousExceptionState == ApplicationExcecutionState.Terminated)
      {
        try { await SuspensionManager.RestoreAsync(); }
        catch (SuspensionManagerException) { }
      }
      // Place the frame in the current Window.    
      Window.Current.Content = rootFrame;
    }
    
    if(rooFrame.Content == null)
    {
      if(!rootFrame.Navigate(typeof()MyPageThatDoesAuth))
      {
        throw new Exception("Failed to create target page")
      }
    }
    // Pass the continuation event args to the target page
    var p= rootFrame.Content as MyPageThatDoesAuth;
    // ContinuationArgs is property that we're added to MyPageThatDoesAuth
    p.ContinuationArgs = (WebAuthenticationBrokerContinuationEventArgs)args;

    // Ensure the current window is active
    Windows.Current.Activate();
  }
}

// Page where WAB was intiated

private WebAuthenticationBrokerContinuationEventArgs _continuationArgs = null;
public WebAuthenticationBrokerContinuationEventArgs ContinuationArgs
{
  get { return _continuationArgs; }
  set{
      _continuationArgs = value;
      ContinueWebAuthentication(_continuationArgs); }
}

public async void ContinueWebAuthentication(WebAuthenticationBrokerContinuationEventArgs args)
{
  WebAuthenticationResult result = args.WebAuthenticationResult;
  
  if (result.ResponseStatus == WebAuthenticationStatus.Succes)
  {
    String outputToken = result.ResponseData.ToString();
    await DoSomethingWithTheTokenAsync(otuputToken);
  }
  else{ /* handle failures (user cancel, HTTP error)*/ }
}
