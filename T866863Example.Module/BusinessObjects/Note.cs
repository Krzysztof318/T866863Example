using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace T866863Example.Module.BusinessObjects
{
	public class Note : BaseObject
	{
		public Note(Session session) : base(session)
		{
			
		}

		[Browsable(false)]
		[Association("Note_Sprawa")]
		public Sprawa Sprawa
		{
			get => GetPropertyValue<Sprawa>(nameof(Sprawa));
			set => SetPropertyValue(nameof(Sprawa), value);
		}

		[ModelDefault("AllowEdit", "False")]
		public int Number
		{
			get => GetPropertyValue<int>(nameof(Number));
			set => SetPropertyValue(nameof(Number), value);
		}

		public string Text
		{
			get => GetPropertyValue<string>(nameof(Text));
			set => SetPropertyValue(nameof(Text), value);
		}
	}
}
