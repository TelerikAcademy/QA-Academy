namespace QAAcademyDemo.TestFramework.Core.Controls
{
    public interface ICheckbox
    {
        bool IsChecked { get; set; }

        void Check(bool shouldCheck);
    }
}