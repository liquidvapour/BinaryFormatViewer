using NUnit.Framework;

namespace SerializationSpike
{
    [TestFixture]
    public abstract class SpecificationBase<T>
    {
        [SetUp]
        public void SetUp()
        {
            try
            {
                SetUpContext();
                sut = CreateSUT();
                Because();
            }
            finally
            {
                OnSetUpCompleted();
            }
        }

        protected T sut;

        protected virtual void SetUpContext()
        {
        }

        protected virtual void OnSetUpCompleted()
        {
        }

        protected abstract T CreateSUT();
        protected abstract void Because();
    }
}