﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;

namespace Orchard
{
    public partial class Step1Page : ContentPage
    {
        public Step1Page()
        {
            InitializeComponent();

            var vm = new Step1VM();

            BindingContext = vm;

            ViewUtils.SetupStepView(_rLayout, _helpSv, _questionContainer, vm.Common.QuestionTapped);

            foreach (var str in OptimizeModeToPickerIdxCov.Names)
            {
                _opMode.Items.Add(str);
            }

            foreach (var str in RowSprayingModeToPickerIdxCov.Names)
            {
                _rowSprayingMode.Items.Add(str);
            }

            var sprayerAIO = new AddItemOption(){ MenuItem = new MenuItem("Sprayers", null) };
            sprayerAIO.AssignCollection(vm.ChosenSprayers);

            var obAIO = new AddItemOption() { MenuItem = new MenuItem("Orchard Blocks", null) };
            obAIO.AssignCollection(vm.ChosenOrchardBlocks);

            var opAIO = new AddItemOption() { MenuItem = new MenuItem("Operators", null) };
            opAIO.AssignCollection(vm.ChosenOperators);

            var mItems = new List<AddItemOption>
            {
                sprayerAIO,
                obAIO,
                opAIO 
            };

            _itemList.ItemsSource = mItems;
            _itemList.ItemTemplate = new DataTemplate(() =>
            {
                var currCell = new TextCellWithDisclosure();
                currCell.SetBinding(TextCell.TextProperty, "MenuItem.MenuTitle");
                currCell.SetBinding(TextCell.DetailProperty, new Binding("NumberOfSelected", 0, new IntToStringConverter()));
                return currCell;
            });
        }

        Step1VM VM
        {
            get
            {
                return (Step1VM)BindingContext;
            }
        }

        public void NextClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send((Page)this, "next");
        }
    }

    public class TextCellWithDisclosure : TextCell
    {

    }

    public class NonScrollingListView : ListView
    {

    }

    public class AddItemOption : NPCBase
    {
        public AddItemOption()
        {
           
        }

        public void AssignCollection<T>(ObservableCollection<T> collection)
        {
            _collection = collection;
            collection.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                RaisePropertyChanged(() => NumberOfSelected);
            };
        }

        public MenuItem MenuItem { get; set; }


        ICollection _collection;

        public int NumberOfSelected
        {
            get
            {
                return _collection.Count;
            }
        }
    }
}

