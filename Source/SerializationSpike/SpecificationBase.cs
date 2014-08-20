/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 02/08/2010
 * Time: 08:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using BinaryFormatViewer;
using NUnit.Framework;

namespace SerializationSpike
{
	[TestFixture]
	public abstract class SpecificationBase<T>
	{
	    protected T sut;
	    
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
	    
	    protected virtual void SetUpContext() { }
	    protected virtual void OnSetUpCompleted() { }
	    protected abstract T CreateSUT();
	    protected abstract void Because();
	}
}
