// Near Field Communications
// NFC provides a connection  between devices that are very close together (Within3-4 centimetres)
// The data is tranferred at a rate of 106,212 or 424 Kbit/second
// NFC operates at slower speeds than bluetooth(BT v2.1 is uo to 2.1Mbit/s) but consumes less power
// and doesnt require pairing
// It is assumed that this data tranfer is intentional so there is not normally any authentication as sych
// The user has positioned their device close to the other device
// The phone can connect to an unpowered NFC chip/tag
// Tags currently support between 96 and 4096 bytes of memory


//---> Trusted Apps: Use NFC to launch an app

Windows.Networking.Proximity.ProximityDevice proximityDevice = 
Windows.Networking.Proximity.proximityDevice.GetDefault();
if(proximityDevice != null)
{
  // The format of the app launch string is: "<args>\tWindows\t<AppName>".
  // The string is tab or null delimited. The <args> strings can be an empty string("").
  string launchArgs = "user=default";
  // The format of the AppName is: PackageFamilyName!PRAID.
  string praid = "MyAppId"; // The Application Id value from your package.Appxmanifest.
  string appName = Windows.ApplicationModel.Package.Current.Id.Familyname + "!" + praid;
  string launchAppMessage = launchArgs + "\tWindows\t" + appName;

  var dataWriter = new Windows.Storage.Streams.DataWriter();
  dataWriter.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf16LE;
  dataWriter.WriteString(launchAppMessage);
  var launchAppPubId = proximityDevice.PublishBinaryMessage("Launch:WriteTag", dataWriter.DetachBuffer());
}

