using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using T866863Example.Module.BusinessObjects;

namespace T866863Example.Module.Controllers
{
	public class NoteCommitController : ObjectViewController<DetailView, Note>
	{
		private bool isNewObject = false;

		protected override void OnActivated()
		{
			base.OnActivated();

			ObjectSpace.Committing += NoteObjectSpaceOnCommitting;
			ObjectSpace.Committed += NoteObjectSpaceOnCommitted;
		}

		private void NoteObjectSpaceOnCommitting(object sender, CancelEventArgs e)
		{
			isNewObject = ObjectSpace.IsNewObject(ViewCurrentObject);
		}

		private void NoteObjectSpaceOnCommitted(object sender, EventArgs e)
		{
			if (!isNewObject) return;
			isNewObject = false;

			var noteOid = ViewCurrentObject.Oid;

			if (ObjectSpace is INestedObjectSpace ns)
			{
				var sprawaObjectSpace = ns.ParentObjectSpace;
				sprawaObjectSpace.Committed += OnCommitted;
			}

			void OnCommitted(object sender2, EventArgs e2)
			{
				((IObjectSpace)sender2).Committed -= OnCommitted;
				SprawaObjectSpaceOnCommitted(((IObjectSpace)sender2), noteOid);
			}
		}

		private void SprawaObjectSpaceOnCommitted(IObjectSpace sprawaObjectSpace, Guid noteOid)
		{
			var os = Global.Application.CreateObjectSpace();
			var note = os.GetObjectByKey<Note>(noteOid);
			note.Number = note.Sprawa.NoteNextNumber++;
			os.CommitChanges();
			sprawaObjectSpace.Refresh();
		}
	}
}
