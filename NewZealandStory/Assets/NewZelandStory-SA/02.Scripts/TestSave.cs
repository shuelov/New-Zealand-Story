using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class TestSave : MonoBehaviour {

	public int score;
	public int point;
	//문자열 저장변수
	string strFilePath;


	// Use this for initialization
	void Start () {
		LoadDate ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SaveData()
	{
		//strFilePath=Application.dataPath+"/Save.dll"; ->실행파일 루트에 저장파일 생성
		//strFilePath="C:/test/Save.dll";->디렉토리 루트를 설정가능
		//실행파일 루트에 저장파일 생성
		strFilePath = "./test/Save.dll"; //<-폴더이름(test)앞에 './'쓰면 그 현재 폴더에 저장한다는 뜻

		//디버깅을 위한 함수로 콘솔 뷰로 문자열 등 여러 데이터를 보낼수 있다.(함수오버로딩) <유니티 콘솔뷰(에러 메세지 알려주는 곳)>
		Debug.Log (strFilePath);

		//파일 스트림을 쓰기 모드로 오픈한다.
		FileStream fs = new FileStream (strFilePath, FileMode.Create, FileAccess.Write);

		//오픈 실패시 함수 종료
		if (fs == null) {
			return;
		}

		//문자열로 저장한다
		//StreamWriter sw=new StreamWriter(fs)
		//sw.writeLine(score); ->한 라인씩 저장
		//sw.WriteLine(point);
		//기계어로 저장한다(보통 이걸 사용)
		BinaryWriter sw = new BinaryWriter (fs);
		sw.Write (score);
		sw.Write (point);

		sw.Close ();
		fs.Close ();
	}

	void LoadDate()
	{
		strFilePath = "./test/Save.dll";

		//해당 파일이 없을 시  함수 종료
		if (File.Exists (strFilePath) == false) {
			return;
		}

		//파일 스트림을 일기 모드로 오픈한다
		FileStream fs = new FileStream (strFilePath, FileMode.Open, FileAccess.Read);
		//오픈 실패시 함수 종료
		if (fs == null) {
			return;
		}
		//문자열을 읽기 위한 StreamReader 생성
		//StreamReader sr=new StreamReader(fs);
		//score=int.Parse (sr.ReadLine()); ->한 라인씩 읽어들이고 인트형 변환
		//Point=int.Parse(sr.ReadLine());
		//기계어를 읽기 위한 StreamReader생성
		BinaryReader sr = new BinaryReader (fs);
		score = sr.ReadInt32 ();
		point = sr.ReadInt32 ();

		sr.Close ();
		fs.Close ();

		//문자열 저장을 확인한다
		Debug.Log ("END");

	}

}
