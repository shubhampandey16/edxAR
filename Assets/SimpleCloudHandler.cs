using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Vuforia {

	public class SimpleCloudHandler : MonoBehaviour, ICloudRecoEventHandler {
		public static int width = Screen.width;
		public static int height = Screen.height;
		private CloudRecoBehaviour mCloudRecoBehaviour;
//		private bool mIsScanning = false;
		private bool mshowGUI = false;
		private bool showMessage = false;
		private bool show3317Btn = false;
		private bool show3339Btn = false;
		private bool show3333Btn = false;
		private bool pano3317Img = false;
		private bool pano3333Img = false;

		private string mTargetMetadata = "";

		private GameObject floorPlaneImageObj;
		private GameObject floorPlaneImageObjA;
		private GameObject floorPlaneImageObjB;
		private GameObject emojiImage;
		private GameObject pano3317Image;
		private GameObject pano3333Image;
		private GameObject button3317Obj;
		private GameObject button3333Obj;
		private GameObject button3339Obj;
		private GameObject successMsg;
		private GameObject reserveButton;

		public Texture2D labelRedTexture;
		public Texture2D buttonRedTexture;
		public Texture2D buttonGreyTexture;
		public Texture2D labelGreenTexture;
		public Texture2D buttonGreenTexture;

		// Use this for initialization
		void Start () {
			floorPlaneImageObjA = GameObject.FindGameObjectWithTag ("floorPlanImg");
			floorPlaneImageObjB = GameObject.FindGameObjectWithTag ("floorPlanImgA");
//			floorPlaneImageObj.SetActive (showMessage);
			floorPlaneImageObj = floorPlaneImageObjA;
			emojiImage = GameObject.FindGameObjectWithTag ("emoji");
			button3317Obj = GameObject.FindGameObjectWithTag ("available3317");
			button3333Obj = GameObject.FindGameObjectWithTag ("available");
			button3339Obj = GameObject.FindGameObjectWithTag ("unavailable");
			pano3317Image = GameObject.FindGameObjectWithTag ("3317pano");
			pano3333Image = GameObject.FindGameObjectWithTag ("3333pano");
			successMsg = GameObject.FindGameObjectWithTag ("successMsg");
			reserveButton = GameObject.FindGameObjectWithTag ("reserveButton");

			successMsg.SetActive (false);
			reserveButton.SetActive (false);
			emojiImage.SetActive (showMessage);
			pano3317Image.SetActive (showMessage);
			pano3333Image.SetActive (showMessage);
			button3317Obj.SetActive (showMessage);
			button3333Obj.SetActive (showMessage);
			button3339Obj.SetActive (showMessage);

			floorPlaneImageObj.SetActive (showMessage);
			floorPlaneImageObjA.SetActive (showMessage);
			floorPlaneImageObjB.SetActive (showMessage);
			mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour> ();
			if (mCloudRecoBehaviour) {
				mCloudRecoBehaviour.RegisterEventHandler (this);
			}
		}

		public void Clicked3317() {
			pano3317Img = true;
			RectTransform pano3317RectTransform = pano3317Image.GetComponent<RectTransform> ();
			RectTransform reserveTransform = reserveButton.GetComponent<RectTransform> ();
			pano3317RectTransform.sizeDelta = new Vector2 (height - 200, width - 100);
			pano3317Image.SetActive (pano3317Img);
			reserveTransform.sizeDelta = new Vector2 (height / 7, width / 7);
			reserveTransform.anchoredPosition = new Vector2 ((int)(height * 0.4), -(int)(width * 0.4));
			reserveButton.SetActive (true);
			Debug.Log ("3317 Clicked!");
		}

		public void Clicked3333() {
			pano3333Img = true;
			RectTransform pano3333RectTransform = pano3333Image.GetComponent<RectTransform> ();
			RectTransform reserveTransform = reserveButton.GetComponent<RectTransform> ();
			pano3333RectTransform.sizeDelta = new Vector2 (height - 200, width - 100);
			reserveTransform.sizeDelta = new Vector2 (height / 10, width / 10);
			reserveTransform.anchoredPosition = new Vector2 ((int)(height * 0.4), -(int)(width * 0.4));
			pano3333Image.SetActive (pano3333Img);
			reserveButton.SetActive (true);
			Debug.Log ("3333 Clicked!");
		}

		public void ClickedReserve() {
			RectTransform successMsgTransform = successMsg.GetComponent<RectTransform> ();
			successMsgTransform.sizeDelta = new Vector2 (height - 100, 50);
			successMsgTransform.anchoredPosition = new Vector2 (0, (int)((width - 20) * 0.45));
			successMsg.SetActive (true);
			floorPlaneImageObj.SetActive (true);
			emojiImage.SetActive (true);
			mshowGUI = false;
			showMessage = true;
			show3339Btn = false;
			show3333Btn = false;
			show3317Btn = false;
			pano3317Img = false;
			pano3333Img = false;
			pano3317Image.SetActive (false);
			pano3333Image.SetActive (false);
			button3317Obj.SetActive (false);
			button3333Obj.SetActive (false);
			button3339Obj.SetActive (false);
			reserveButton.SetActive (false);

		}

		public void OnInitialized() {
			Debug.Log ("Cloud Reco Initialized");
		}

		public void OnInitError(TargetFinder.InitState initError) {
			Debug.Log ("Cloud Reco Init Error " + initError.ToString ());
		}

		public void OnUpdateError(TargetFinder.UpdateState updateError) {
			Debug.Log ("Cloud Reco Update Error " + updateError.ToString ());
		}

		public void OnStateChanged(bool scanning) {
//			mIsScanning = scanning;
			if (scanning) {
				ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
				tracker.TargetFinder.ClearTrackables (false);
				mshowGUI = false;
				showMessage = false;
				show3339Btn = false;
				show3333Btn = false;
				show3317Btn = false;
				pano3317Img = false;
				pano3333Img = false;
				floorPlaneImageObj.SetActive (showMessage);
				emojiImage.SetActive (showMessage);
				pano3317Image.SetActive (showMessage);
				pano3333Image.SetActive (showMessage);
				button3317Obj.SetActive (showMessage);
				button3333Obj.SetActive (showMessage);
				button3339Obj.SetActive (showMessage);
				reserveButton.SetActive (showMessage);
				successMsg.SetActive (showMessage);
			}
		}

		public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult) {
			mTargetMetadata = targetSearchResult.MetaData;
			mCloudRecoBehaviour.CloudRecoEnabled = false;
			Debug.Log ("Detected" + mTargetMetadata);
			mshowGUI = true;
			showMessage = false;
			show3339Btn = false;
			show3333Btn = false;
			show3317Btn = false;
			pano3317Img = false;
			pano3333Img = false;
		}

		void OnGUI() {
			
			GUIStyle backButtonStyle = new GUIStyle (GUI.skin.textField);
			backButtonStyle.fontSize = 30;
			backButtonStyle.richText = true;
			backButtonStyle.normal.background = buttonGreyTexture;
			backButtonStyle.alignment = TextAnchor.MiddleCenter;

			GUIStyle myButtonStyleRed = new GUIStyle (GUI.skin.textField);
			myButtonStyleRed.fontSize = 35;
			myButtonStyleRed.richText = true;
			myButtonStyleRed.fontStyle = FontStyle.Bold;
			//			myButtonStyleRed.normal.background = MakeTex (1000, 50, new Color(1.0f, 0f, 0f, 0.7f));
			myButtonStyleRed.normal.background = buttonRedTexture;
			myButtonStyleRed.alignment = TextAnchor.MiddleCenter;

			GUIStyle myBoxStyleRed = new GUIStyle (GUI.skin.box);
			//			myBoxStyleRed.normal.background = MakeTex(1000, 120, new Color(1.0f, 1.0f, 1.0f, 0.7f));
			myBoxStyleRed.normal.background = labelRedTexture;
			myBoxStyleRed.alignment = TextAnchor.MiddleCenter;
			myBoxStyleRed.fontSize = 45;
			myBoxStyleRed.fontStyle = FontStyle.Bold;

			GUIStyle myButtonStyleGreen = new GUIStyle (GUI.skin.textField);
			myButtonStyleGreen.fontSize = 35;
			myButtonStyleGreen.richText = true;
			myButtonStyleGreen.fontStyle = FontStyle.Bold;
			//			myButtonStyleGreen.normal.background = MakeTex (1000, 50, new Color(1.0f, 0f, 0f, 0.7f));
			myButtonStyleGreen.normal.background = buttonGreenTexture;
			myButtonStyleGreen.alignment = TextAnchor.MiddleCenter;

			GUIStyle myBoxStyleGreen = new GUIStyle (GUI.skin.box);
			//			myBoxStyleRed.normal.background = MakeTex(1000, 120, new Color(1.0f, 1.0f, 1.0f, 0.7f));
			myBoxStyleGreen.normal.background = labelGreenTexture;
			myBoxStyleGreen.alignment = TextAnchor.MiddleCenter;
			myBoxStyleGreen.fontSize = 30;
			myBoxStyleGreen.fontStyle = FontStyle.Bold;

			if (mshowGUI) {
				int boxWidth = (int)(width/2 - 100);
				int boxHeight = height - 100;
				int buttonWidth = width / 4 - 100;
				int buttonHeight = height - 100;
				RectTransform emojiRectTransform = emojiImage.GetComponent<RectTransform> ();

				RectTransform button3317 = button3317Obj.GetComponent<RectTransform> ();
				RectTransform button3339 = button3339Obj.GetComponent<RectTransform> ();
				RectTransform button3333 = button3333Obj.GetComponent<RectTransform> ();
				button3317.sizeDelta = new Vector2 ((int)((height - 20)*0.0275), (int)((width - 20)*0.1167));
				button3339.sizeDelta = new Vector2 ((int)((height - 20)*0.0275), (int)((width - 20)*0.1167));
				button3333.sizeDelta = new Vector2 ((int)((height - 20)*0.03125), (int)((width - 20)*0.2333));

				GUI.contentColor = Color.black;
				if (mTargetMetadata.Trim ().ToUpper ().Equals ("3333")) {
					transform.Rotate (0, 0, 30, Space.World);  
					GUI.Box (new Rect (50, 25, boxHeight, boxWidth), "Conference room number 3333 is available." +
					"\n Available till 12PM." +
					"\n Not available from 12PM to 2PM.", myBoxStyleGreen);
					if (GUI.Button (new Rect (50, boxWidth + 50, buttonHeight, buttonWidth), "Click for a virtual tour", myButtonStyleGreen)) {
						showMessage = true;
						emojiRectTransform.anchoredPosition = new Vector2 ((int)((height - 20) * 0.125), (int)((width - 20) * 0.05));
						Clicked3333 ();
					}
				} else if (mTargetMetadata.Trim ().ToUpper ().Equals ("3339")) {
					
					GUI.Box (new Rect (50, 25, boxHeight, boxWidth), "Conference room number 3339 is not available." +
					"\n Not available until 3PM.", myBoxStyleRed);
					
					if (GUI.Button (new Rect (50, boxWidth + 50, buttonHeight, buttonWidth), "Find nearby conference rooms", myButtonStyleRed)) {
						showMessage = true;
						floorPlaneImageObj = floorPlaneImageObjA;

						emojiRectTransform.anchoredPosition = new Vector2 (-(int)((height - 20) * 0.0375), (int)((width - 20) * 0.035));
						button3317.anchoredPosition = new Vector2 ((int)((height - 20) * 0.14375), (int)((width - 20) * 0.33));
						button3333.anchoredPosition = new Vector2 ((int)((height - 20) * 0.1125), (int)((width - 20) * 0.2717));
						button3339.anchoredPosition = new Vector2 ((int)((height - 20) * 0.04125), (int)((width - 20) * 0.2133));
						show3317Btn = true;
						show3333Btn = true;
						show3339Btn = false;
					}

				} else if (mTargetMetadata.Trim ().ToUpper ().Equals ("3310")) {
					
					GUI.Box (new Rect (50, 25, boxHeight, boxWidth), "Room number 3310 is a non-reservable private room.", myBoxStyleRed);

					if (GUI.Button (new Rect (50, boxWidth + 50, buttonHeight, buttonWidth), "Find nearby conference rooms", myButtonStyleRed)) {
						showMessage = true;
						floorPlaneImageObj = floorPlaneImageObjB;

						emojiRectTransform.anchoredPosition = new Vector2 (0, -(int)((width - 20) * 0.04167));
						button3317.anchoredPosition = new Vector2 (-(int)((height - 20) * 0.19125), (int)((width - 20) * 0.14167));
						button3333.anchoredPosition = new Vector2 (-(int)((height - 20) * 0.15625), (int)((width - 20) * 0.2));
						button3339.anchoredPosition = new Vector2 ((int)((height - 20) * 0.00625), (int)((width - 20) * 0.2583));
						show3317Btn = true;
						show3333Btn = true;
						show3339Btn = true;
					}

				} else if (mTargetMetadata.Trim ().ToUpper ().Equals ("CSB3EW")) {
					
					GUI.Box (new Rect (50, 25, boxHeight, boxWidth), "Welcome to CSB 3rd floor East Wing.", myBoxStyleGreen);

					if (GUI.Button (new Rect (50, boxWidth + 50, buttonHeight, buttonWidth), "Find nearby conference rooms", myButtonStyleGreen)) {
						showMessage = true;
						floorPlaneImageObj = floorPlaneImageObjA;

						emojiRectTransform.anchoredPosition = new Vector2 (-(int)((height - 20) * 0.3125), -(int)((width - 20) * 0.2333));
						button3317.anchoredPosition = new Vector2 ((int)((height - 20) * 0.14375), (int)((width - 20) * 0.33));
						button3333.anchoredPosition = new Vector2 ((int)((height - 20) * 0.1125), (int)((width - 20) * 0.2717));
						button3339.anchoredPosition = new Vector2 ((int)((height - 20) * 0.04125), (int)((width - 20) * 0.2133));
						show3317Btn = true;
						show3333Btn = true;
						show3339Btn = true;
					}
				
				} else if (mTargetMetadata.Trim ().ToUpper ().Equals ("1264")) {
					transform.Rotate (0, 0, 30, Space.World);  
					GUI.Box (new Rect (50, 25, boxHeight, boxWidth), "Conference room number 1264 is available." +
					"\n Sastry is travelling today. :)", myBoxStyleGreen);
					if (GUI.Button (new Rect (50, boxWidth + 50, buttonHeight, buttonWidth), "Come back for more!", myButtonStyleGreen)) {
					}
				}

				RectTransform imageRectTransform = floorPlaneImageObj.GetComponent<RectTransform> ();
				imageRectTransform.sizeDelta = new Vector2 (height - 20, width - 20);
//				imageRectTransform.anchoredPosition = new Vector2 (10, 10);



				if (showMessage) {
//					GUI.Label (new Rect (100, 370, 1000, 50), "Thanks, come back later for more!", myButtonStyle);
					mshowGUI = false;
					floorPlaneImageObj.SetActive (showMessage);
					emojiImage.SetActive (showMessage);
					button3317Obj.SetActive (show3317Btn);
					button3333Obj.SetActive (show3333Btn);
					button3339Obj.SetActive (show3339Btn);
				}
				GUI.contentColor = Color.black;
			}
			if (GUI.Button (new Rect (height - 150, 10, 100, 80), "Restart", backButtonStyle)) {
				mCloudRecoBehaviour.CloudRecoEnabled = true;
			}
		}

		// Update is called once per frame
		void Update () {
//			floorPlaneImageObj.transform.eulerAngles = new Vector3 (40f, 0, 0);
		}

		private Texture2D MakeTex( int width, int height, Color col )
		{
			Color[] pix = new Color[width * height];
			for( int i = 0; i < pix.Length; ++i )
			{
				pix[ i ] = col;
			}
			Texture2D result = new Texture2D( width, height );

			result.SetPixels( pix );
			result.Apply();
			return result;
		}
	}
}