using System;
using System.Collections.Generic;
using UIKit;

namespace PodcastRadio.iOS.Models
{
    public class CategoryPickerModel : UIPickerViewModel
    {
        private List<string> _categories;
        private EventHandler<string> _selectedCategory;

        public CategoryPickerModel(List<string> categories, EventHandler<string> selectedCategory)
        {
            _categories = categories;
            _selectedCategory = selectedCategory;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component) => _categories[(int)row];

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component) => (nint)_categories?.Count;

        public override nint GetComponentCount(UIPickerView pickerView) => 1;

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            _selectedCategory?.Invoke(this, _categories[(int)pickerView.SelectedRowInComponent(component)]);
        }
    }
}
