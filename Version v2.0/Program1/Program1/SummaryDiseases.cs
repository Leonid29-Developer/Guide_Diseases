using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program1
{
    class SummaryDiseases
    {
        public string Disease { get; private set; }
        public byte[] Image { get; private set; }
        public List<Descriptions> Description { get; private set; }
        public List<Agricultures> Agriculture { get; private set; }
        public List<PreventionMethods> PreventionMethod { get; private set; }
        public List<RecognitionMethods> RecognitionMethod { get; private set; }
        public List<StruggleMethods> StruggleMethod { get; private set; }

        public SummaryDiseases
            (
                string NewDisease, byte[] NewImage, List<Descriptions> NewDescription, List<Agricultures> NewAgriculture,
                List<PreventionMethods> NewPreventionMethod, List<RecognitionMethods> NewRecognitionMethod, List<StruggleMethods> NewStruggleMethod
            )
        {
            Disease = NewDisease; Image = NewImage; Description = NewDescription; Agriculture = NewAgriculture;
            PreventionMethod = NewPreventionMethod; RecognitionMethod = NewRecognitionMethod; StruggleMethod = NewStruggleMethod;
        }
    }

    class Descriptions
    {
        public string Text { get; private set; }
        public Descriptions(string Description) { Text = Description; }
    }

    class Agricultures
    {
        public string Text { get; private set; }
        public Agricultures(string Agriculture) { Text = Agriculture; }
    }

    class PreventionMethods
    {
        public string Text { get; private set; }
        public PreventionMethods(string PreventionMethod) { Text = PreventionMethod; }
    }

    class RecognitionMethods
    {
        public string Text { get; private set; }
        public RecognitionMethods(string RecognitionMethod) { Text = RecognitionMethod; }
    }

    class StruggleMethods
    {
        public string Text { get; private set; }
        public StruggleMethods(string StruggleMethod) { Text = StruggleMethod; }
    }
}
