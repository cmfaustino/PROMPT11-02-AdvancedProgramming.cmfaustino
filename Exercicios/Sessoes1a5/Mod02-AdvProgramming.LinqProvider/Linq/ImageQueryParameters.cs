namespace Mod02_AdvProgramming.LinqProvider.Linq
{
    using System;
    using Mod02_AdvProgramming.LinqProvider.ImagesMetadata;

    internal class ImageQueryParameters
    {
            public ImagesIndexer.QueryFilter Filter { get; set; }
            public int MaxImages { get; set; }

            public ImageQueryParameters()
            {
                this.Filter = new ImagesIndexer.QueryFilter();
                this.MaxImages = -1;
            }

            public override string ToString()
            {
                if (this.MaxImages >= 0)
                {
                    return String.Format("{0}Maximum returned images: {1}", Filter.ToString(), this.MaxImages);
                }
                else
                {
                    return Filter.ToString();
                }
            }
        }
}