using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AppBar.ViewModels
{
	class MainWindowViewModel : BaseViewModel
	{
		#region Private Variables

		#endregion

		#region Private Properties
		
		private int _contentPadding=10;
		private int _borderSize=3;
		private SolidColorBrush _borderColor=Brushes.GreenYellow;
		private SolidColorBrush _backgroundColor= Brushes.DarkSeaGreen;
		private int _minWidth = 16;
		private int _minHeight = 16;
		private int _width = 32;
		private int _height = 32;
		#endregion

		#region Public Properties

		/// <summary>
		/// The AppBar minimum width
		/// </summary>
		public int MinWidth
		{
			get => _minWidth;
			set
			{
				if (value != _minWidth)
					_minWidth = value;
			}
		}

		/// <summary>
		/// The AppBar minimum height
		/// </summary>
		public int MinHeight
		{
			get => _minHeight;
			set
			{
				if (value != _minHeight)
					_minHeight = value;
			}
		}

		/// <summary>
		/// The AppBar width
		/// </summary>
		public int Width
		{
			get => _width;
			set
			{
				if (value != _width)
					_width = value;
			}
		}

		/// <summary>
		/// The AppBar height
		/// </summary>
		public int Height
		{
			get => _height;
			set
			{
				if (value != _height)
					_height = value;
			}
		}

		/// <summary>
		/// The internal content padding of the AppBar
		/// </summary>
		public int ContentPadding {
			get => _contentPadding;
			set {
				if (value != _contentPadding)
					_contentPadding = value;
			}
		}

		/// <summary>
		/// The outer AppBar Border Size
		/// </summary>
		public int BorderSize
		{
			get => _borderSize;
			set
			{
				if (value != _borderSize)
					_borderSize = value;
			}
		}

		/// <summary>
		/// The outer AppBar Border Thickness
		/// </summary>
		public Thickness BorderThickness
		{
			get => new Thickness(_borderSize);
		}

		/// <summary>
		/// The AppBar Background Color
		/// </summary>
		public SolidColorBrush BackgroundColor
		{
			get => _backgroundColor;
			set
			{
				if (value != _backgroundColor)
					_backgroundColor = value;
			}
		}

		/// <summary>
		/// The outer AppBar Border Color
		/// </summary>
		public SolidColorBrush BorderColor
		{
			get => _borderColor;
			set
			{
				if (value != _borderColor)
					_borderColor = value;
			}
		}

		


		#endregion

		#region Public Commands

		/// <summary>
		/// The command to hide the AppBar
		/// </summary>
		public ICommand HideAppBarCommand { get; set; }

		/// <summary>
		/// The command to Show the AppBar
		/// </summary>
		public ICommand ShowAppBarcommand { get; set; }

		/// <summary>
		/// The command to expand the AppBar to full display width
		/// </summary>
		public ICommand MaximizeAppBarCommand { get; set; }
		
		/// <summary>
		/// The command to close the AppBar
		/// </summary>
		public ICommand CloseCommand { get; set; }

		/// <summary>
		/// The command to show the menu of the AppBar
		/// </summary>
		public ICommand MenuCommand { get; set; }

		#endregion
	}
}
