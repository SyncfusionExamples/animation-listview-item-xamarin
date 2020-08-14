# How to animate newly rendering ListView items in Xamarin.Forms (SfListView)

You can animate the [ListViewItem](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.ListViewItem.html) only when loading first time not on reusing by using Model class property in Xamarin.Forms [SfListView](https://help.syncfusion.com/xamarin/listview/overview).

You can refer to the following documentation to animate the **ListViewItems** ,

[https://www.syncfusion.com/kb/9537/how-to-animate-xamarin-listview-items](https://www.syncfusion.com/kb/9537/how-to-animate-xamarin-listview-items)

**XAML**

Bind [Command](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.button.command#Xamarin_Forms_Button_Command) for [Button](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/button) to add items to the collection.

``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:ListViewXamarin"
             xmlns:syncfusion="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms"
             x:Class="ListViewXamarin.MainPage" Padding="0,20,0,0">
    <ContentPage.BindingContext>
        <local:ContactsViewModel/>
    </ContentPage.BindingContext>
    <ContentPage.Behaviors>
        <local:Behaviour/>
    </ContentPage.Behaviors>
    <ContentPage.Content>
        <StackLayout>
            <Button x:Name="addButton" Text="Add items" Command="{Binding AddItemsCommand}" HeightRequest="50"/>
            <syncfusion:SfListView x:Name="listView" ItemSize="60" ItemsSource="{Binding ContactsInfo}">
                <syncfusion:SfListView.ItemTemplate >
                    <DataTemplate>
                        <Grid x:Name="grid">
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="70" />
                                <ColumnDefinition Width="*" />
                            </Grid.ColumnDefinitions>
                            <Image Source="{Binding ContactImage}" VerticalOptions="Center" HorizontalOptions="Center" HeightRequest="50" WidthRequest="50"/>
                            <Grid Grid.Column="1" RowSpacing="1" Padding="10,0,0,0" VerticalOptions="Center">
                                <Label LineBreakMode="NoWrap" TextColor="#474747" Text="{Binding ContactName}"/>
                                <Label Grid.Row="1" Grid.Column="0" TextColor="#474747" LineBreakMode="NoWrap" Text="{Binding ContactNumber}"/>
                            </Grid>
                        </Grid>
                    </DataTemplate>
                </syncfusion:SfListView.ItemTemplate>
            </syncfusion:SfListView>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

**C#**

Add items in the command execution method.

``` c#
namespace ListViewXamarin
{
    public class ContactsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Contacts> ContactsInfo { get; set; }
        public Command AddItemsCommand { get; set; }

        public ContactsViewModel()
        {
            ContactsInfo = new ObservableCollection<Contacts>();
            AddItemsCommand = new Command(AddItems);
            GenerateInfo();
        }

        private void AddItems()
        {
            Random r = new Random();
            for (int i = 0; i < 5; i++)
            {
                var contact = new Contacts(CustomerNames[r.Next(0, 30)], r.Next(720, 799).ToString() + " - " + r.Next(3010, 3999).ToString());
                contact.ContactImage = ImageSource.FromResource("ListViewXamarin.Images.Image" + r.Next(0, 28) + ".png");
                ContactsInfo.Add(contact);
            }
        }
    }
}
```
**C#**

Defined **IsAnimated** property in the Model class. Get the item data from the [BindingContext](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.ListViewItem~BindingContext.html) of the **ListViewItemExt**. Based on the **IsAnimated** property you can animate the **ListViewItem** only once.

``` c#
public class ListViewItemExt : ListViewItem
{
    public ListViewItemExt()
    {
    }

    protected override void OnItemAppearing()
    {
        var item = this.BindingContext as Contacts;
        if (!item.IsAnimated)
        {
            this.Opacity = 0;
            this.FadeTo(1, 400, Easing.SinInOut);
            item.IsAnimated = true;
        }
        base.OnItemAppearing();
    }
}
```
**Output**

![AnimationForNewItem](https://github.com/SyncfusionExamples/animation-listview-item-xamarin/blob/master/ScreenShot/AnimationForNewItem.gif)
