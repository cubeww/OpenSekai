namespace Sekai.Mysekai
{
	public interface ISlideNoticeCell
	{
		void Open();

		void Close();

		void Sleep();

		void Wakeup();

		void Setup(NoticeViewDataBase viewData);
	}
}
