//--> Implementing the INotifyPropertyChanged (The Non-Optimal Way)

public class ItemViewModel : INotifyPropertyChanged 
{
  private string _manufacturer;
  private string _model;
  private string _color;
  private int _year;
  
  public string manufacturer
  {
    get { return _manufacturer;}  
    set {
      _manufacturer = value;
      NotifyPropertyChanged("manufacturer");  }
  }  
  public string model
  {
    get { return _model; }
    set {
      _model = value;
      NotifyPropertyChanged("model");  }
  }  
  public string color
  {
    get { return _color; }
    set {
      _color = value;
      NotifyPropertyChanged("color");  }
  }  
  public int year
  {
    get { return _year;  } 
    set {
      _year = value;
      NotifyPropertyChanged("color");  }
  }  
  // Property Change Logic
  public event PropertyChangedEventHandler PropertyChanged;  
  private void NotifyPropertyChanged(string propertyName)
  {      
    if (PropertyChanged != null)       
      {
        PropertyChanged(this, new  ropertyChangedEventArgs(propertyName));  
      }
  }
  
// class ends here  
} 


//--> Implementing the INotifyPropertyChanged (The Optimal Way)

public class ItemViewModel: INotifyPropertyChanged
{
  public event PropertyChangedEventHandler PropertyChanged;
  private string model;
  private string manufacturer;
  private string color;
  private int year;

  public string Model
  {
    get { return model; }
    set { model = value; }
  }
  protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
  {
    if (object.Equals(storage, value)) return false;
    storage = value;
    this.OnPropertyChanged(propertyName);
    return true;
  }
  public void OnPropertyChanged(string propertyName)
  {
    var eventHandler = this.PropertyChanged;
    if (eventHandler != null)
    {
      eventHandler(this, new PropertyChangedEventArgs(propertyName) );
    }
  }
  
// class ends here  
}