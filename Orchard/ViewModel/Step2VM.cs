using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Orchard
{
    public class Step2VM : NPCBase
    {
        public Step2VM()
        {
            Common = new StepVMCommon("Step2Questions.txt", "Step2HelpTexts.txt", "Tree Shape and Density");

            DenseImg = ImageSource.FromResource("Orchard.Images.Density.pre-dense.jpg");
            OpenImg = ImageSource.FromResource("Orchard.Images.Density.pre-open.jpg");
            ExtreOpenImg = ImageSource.FromResource("Orchard.Images.Density.pre-extreOpen.jpg");
        }

        public StepVMCommon Common{ get; set; }

        GrowthStage _currGrowStage;

        public GrowthStage CurrGrowStage
        {
            get { return _currGrowStage; }
            set
            {
                SetProperty(ref _currGrowStage, value);
                switch (value)
                {
                    case GrowthStage.PrePetaFall:
                        {
                            DenseImg = ImageSource.FromResource("Orchard.Images.Density.pre-dense.jpg");
                            OpenImg = ImageSource.FromResource("Orchard.Images.Density.pre-open.jpg");
                            ExtreOpenImg = ImageSource.FromResource("Orchard.Images.Density.pre-extreOpen.jpg");
                        }
                        break;
                    case GrowthStage.PostPetaFall:
                        {
                            DenseImg = ImageSource.FromResource("Orchard.Images.Density.post-dense.jpg");
                            OpenImg = ImageSource.FromResource("Orchard.Images.Density.post-open.jpg");
                            ExtreOpenImg = ImageSource.FromResource("Orchard.Images.Density.post-extreOpen.jpg");
                        }
                        break;
                }
            }
        }

        ImageSource _denseImg;

        public ImageSource DenseImg
        {
            get { return _denseImg; }
            set { SetProperty(ref _denseImg, value); }
        }

        ImageSource _openImg;

        public ImageSource OpenImg
        {
            get { return _openImg; }
            set { SetProperty(ref _openImg, value); }
        }

        ImageSource _extreOpenImg;

        public ImageSource ExtreOpenImg
        {
            get { return _extreOpenImg; }
            set { SetProperty(ref _extreOpenImg, value); }
        }

        public enum GrowthStage
        {
            PrePetaFall,
            PostPetaFall
        }
    }
}

