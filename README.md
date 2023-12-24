# Loading Generic.xaml in SOLIDWORKS and PDM add-ins 

Because SW and PDM add-ins do not create a WPF application by default, you need to create or get the[ WPF application](https://learn.microsoft.com/en-us/dotnet/api/system.windows.application?view=windowsdesktop-8.0) in the SW app domain. Add this code in your ConnectToSW or the constructor of your PDM add-in

```

  // assume addinDirectory is the DirectoryInfo where your add-ins files are stored.
    var dll = addinDirectory.GetFiles().ToList().Where(x => x.Name.Equals("BlueByte.Wpf.Controls.DynamicTextBox.dll", StringComparison.OrdinalIgnoreCase)).First();
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            Uri uri = new Uri($"pack://application:,,,/BlueByte.Wpf.Controls.DynamicTextBox;component/" + @"Themes/Generic.xaml", UriKind.Absolute);
            resourceDictionary.Source = uri;
            if (System.Windows.Application.Current.Resources.MergedDictionaries.Contains(resourceDictionary) == false)
                System.Windows.Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

```

If you're using [BlueByte.SOLIDWORKS.PDMSDK](https://github.com/BlueByteSystemsInc/SOLIDWORKS-PDM-API-SDK), use this:

```
protected override void OnLoadAdditionalAssemblies(DirectoryInfo addinDirectory)
        {
            base.OnLoadAdditionalAssemblies(addinDirectory);

            if (System.Windows.Application.Current == null)
            {
                var sys = new System.Windows.Application();
                sys.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;
            }

                var dll = addinDirectory.GetFiles().ToList().Where(x => x.Name.Equals("BlueByte.Wpf.Controls.DynamicTextBox.dll", StringComparison.OrdinalIgnoreCase)).First();
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            Uri uri = new Uri($"pack://application:,,,/BlueByte.Wpf.Controls.DynamicTextBox;component/" + @"Themes/Generic.xaml", UriKind.Absolute);
            resourceDictionary.Source = uri;
            if (System.Windows.Application.Current.Resources.MergedDictionaries.Contains(resourceDictionary) == false)
                System.Windows.Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);


        }


```


# Demo

https://github.com/BlueByteSystemsInc/BlueByte.Wpf.Controls.DynamicTextBox/assets/16106587/165d33c5-f6e9-4241-8e70-1e9964042009

