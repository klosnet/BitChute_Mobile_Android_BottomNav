package md577ffdca814d95cac9efa33f0b39853f4;


public class TheFragment4_ExtWebViewClient
	extends android.webkit.WebViewClient
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPageFinished:(Landroid/webkit/WebView;Ljava/lang/String;)V:GetOnPageFinished_Landroid_webkit_WebView_Ljava_lang_String_Handler\n" +
			"";
		mono.android.Runtime.register ("BottomNavigationViewPager.Fragments.TheFragment4+ExtWebViewClient, BottomNavigationViewPager", TheFragment4_ExtWebViewClient.class, __md_methods);
	}


	public TheFragment4_ExtWebViewClient ()
	{
		super ();
		if (getClass () == TheFragment4_ExtWebViewClient.class)
			mono.android.TypeManager.Activate ("BottomNavigationViewPager.Fragments.TheFragment4+ExtWebViewClient, BottomNavigationViewPager", "", this, new java.lang.Object[] {  });
	}


	public void onPageFinished (android.webkit.WebView p0, java.lang.String p1)
	{
		n_onPageFinished (p0, p1);
	}

	private native void n_onPageFinished (android.webkit.WebView p0, java.lang.String p1);

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
