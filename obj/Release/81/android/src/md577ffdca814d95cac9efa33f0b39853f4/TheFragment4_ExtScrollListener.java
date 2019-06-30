package md577ffdca814d95cac9efa33f0b39853f4;


public class TheFragment4_ExtScrollListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnScrollChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onScrollChange:(Landroid/view/View;IIII)V:GetOnScrollChange_Landroid_view_View_IIIIHandler:Android.Views.View/IOnScrollChangeListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("BottomNavigationViewPager.Fragments.TheFragment4+ExtScrollListener, BottomNavigationViewPager", TheFragment4_ExtScrollListener.class, __md_methods);
	}


	public TheFragment4_ExtScrollListener ()
	{
		super ();
		if (getClass () == TheFragment4_ExtScrollListener.class)
			mono.android.TypeManager.Activate ("BottomNavigationViewPager.Fragments.TheFragment4+ExtScrollListener, BottomNavigationViewPager", "", this, new java.lang.Object[] {  });
	}


	public void onScrollChange (android.view.View p0, int p1, int p2, int p3, int p4)
	{
		n_onScrollChange (p0, p1, p2, p3, p4);
	}

	private native void n_onScrollChange (android.view.View p0, int p1, int p2, int p3, int p4);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
