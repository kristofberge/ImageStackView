using System;
using Xamarin.Forms;

namespace ImageStackView
{
    public class ImageStackView : ContentView
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create("Image", typeof(string), typeof(ImageStackView), default(string));

        public static readonly BindableProperty LayersProperty =
            BindableProperty.Create("Layers", typeof(int), typeof(ImageStackView), 3);

        public int Layers
        {
            get => (int)GetValue(LayersProperty);
            set => SetValue(LayersProperty, value);
        }

        public string Image
        {
            get => (string)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public ImageStackView()
        {
            Content = new AbsoluteLayout();
        }


        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            try
            {
                var absoluteLayout = Content as AbsoluteLayout;

                if (!string.IsNullOrWhiteSpace(Image))
                {
                    var topWidth = width * 0.8;
                    var topHeight = height;
                    var bottomWidth = width * 0.6;
                    var bottomHeight = height * 0.6;
                    var spaceForOtherLayers = width * 0.2;
                    var spacePerLayer = spaceForOtherLayers / (Layers - 1);
                    var widthDecreasePerLayer = (topWidth - bottomWidth) / (Layers - 1);
                    var heightDecreasePerLayer = (topHeight - bottomHeight) / (Layers - 1);

                    for (int layer = Layers; layer >= 0; layer--)
                    {
                        var image = new Image
                        {
                            Source = Image,
                            Aspect = Aspect.AspectFill
                        };

                        var frame = new Frame
                        {
                            Padding = 0,
                            HasShadow = false,
                            CornerRadius = 10,
                            IsClippedToBounds = true,
                            Content = image
                        };

                        var imageWidth = topWidth - (widthDecreasePerLayer * (layer - 1));
                        var imageHeight = topHeight - (heightDecreasePerLayer * (layer - 1));
                        var imageLeftEdge = width - (spacePerLayer * (layer - 1));
                        var imageX = imageLeftEdge - imageWidth;
                        AbsoluteLayout.SetLayoutBounds(frame, new Rectangle(imageX, 0.5, imageWidth, imageHeight));
                        AbsoluteLayout.SetLayoutFlags(frame, AbsoluteLayoutFlags.YProportional);

                        absoluteLayout.Children.Add(frame);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            base.LayoutChildren(x, y, width, height);
        }
    }
}
