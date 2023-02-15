using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace JSONtest.Models
{
    class ToDoModel:INotifyPropertyChanged
    {
		[JsonProperty(PropertyName= "creationDate")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        private string _text;
        private bool _isDone;

        public event PropertyChangedEventHandler PropertyChanged;

        [JsonProperty(PropertyName = "isDone")]
        public bool IsDone
		{
			get { return _isDone; }
			set 
			{
				if (_isDone == value) return;
				_isDone = value;
				onPropertyChanged("IsDone");
			}
		}


        [JsonProperty(PropertyName = "text")]
        public string Text
		{
			get { return _text; }
			set 
			{
				if (_text == value) return; 
				_text = value;
				onPropertyChanged("Text");
			}
		}

		protected virtual void onPropertyChanged(string propertyName="")
		{
			//if (PropertyChanged!=null)
			//{
			//   PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			//}

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			
		}

	}
}
