# Gaugable
Xamarin Forms Gauge Control

Warning: This is alpha code. Not ready for the big times.

Very simple gauge control. Supports:
-Min and Max progress bar.
-Basic ranges
-Major and minor ticks (enable/disabe and increments).

Example XAML:
```XML
<controls:Gauge
    x:Name="gauge"
    BackgroundColor="Gray"
    AxisColor="Navy" 
    MajorTicks="true" 
    MinorTicks="true" 
    MinorTickIncrement="15" 
    MajorTickIncrement="60" 
    MinProgress="0" 
    MaxProgress="180" 
    Color="Fuchsia" 
    HeightRequest="100" 
    HorizontalOptions="FillAndExpand" 
    Progress="{Binding Progress}">
  <controls:Gauge.RangeDefinition>
    <controls:Range Min="60" Max="120" Color="Green" />
  </controls:Gauge.RangeDefinition>
</controls:Gauge>
```

Most everything is bindable.
