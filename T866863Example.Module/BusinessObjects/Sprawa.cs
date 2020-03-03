using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace T866863Example.Module.BusinessObjects
{
	[DefaultClassOptions]
	[DefaultProperty(nameof(Title))]
	public class Sprawa : BaseObject
	{
		public Sprawa(Session session) : base(session)
		{
			
		}

		public string Title
		{
			get => GetPropertyValue<string>(nameof(Title));
			set => SetPropertyValue(nameof(Title), value);
		}

		public override void AfterConstruction()
		{
			base.AfterConstruction();

			NoteNextNumber = 1;
		}

		[Browsable(false)]
		public int NoteNextNumber
		{
			get => GetPropertyValue<int>(nameof(NoteNextNumber));
			set => SetPropertyValue(nameof(NoteNextNumber), value);
		}

		[Association("Note_Sprawa"), Aggregated] 
		public XPCollection<Note> Notes => GetCollection<Note>(nameof(Notes));
	}
}
