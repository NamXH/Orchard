using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace Orchard
{
    public class Step1VM : NPCBase
    {
        public Step1VM()
        {
            Common = new StepVMCommon("Step1Questions.txt", "Step1HelpTexts.txt", "Basic Information");
        }

        public StepVMCommon Common
        {
            get;
            set;
        }

        OptimizeMode _currOptimizeMode;

        public OptimizeMode CurrOptimizeMode
        {
            get { return _currOptimizeMode; }
            set { SetProperty(ref _currOptimizeMode, value); }
        }

        RowSprayingMode _currRowSprayingMode;

        public RowSprayingMode CurrRowSprayingMode
        {
            get { return _currRowSprayingMode; }
            set { SetProperty(ref _currRowSprayingMode, value); }
        }

        ObservableCollection<Sprayer> _chosenSprayers = new ObservableCollection<Sprayer>();

        public ObservableCollection<Sprayer> ChosenSprayers
        {
            get { return _chosenSprayers; }
        }

        ObservableCollection<OrchardBlock> _chosenOrchardBlocks = new ObservableCollection<OrchardBlock>();

        public ObservableCollection<OrchardBlock> ChosenOrchardBlocks
        {
            get { return _chosenOrchardBlocks; }
        }

        ObservableCollection<Operator> _chosenOperators = new ObservableCollection<Operator>();

        public ObservableCollection<Operator> ChosenOperators
        {
            get { return _chosenOperators; }
        }

        public enum OptimizeMode
        {
            OptimizedRate,
            LabelRate
        }

        public enum RowSprayingMode
        {
            EveryRow,
            AlternateRow
        }
    }
}
