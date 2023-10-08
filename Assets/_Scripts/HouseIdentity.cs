using UnityEngine;


public class HouseIdentity : MonoBehaviour 
{
	[SerializeField]
	private int id = 1;
	public int Id { get { return id; } }

	public void PlaySceneHouse(int houseID) 
	{
		GameManager.instance.LoadScene(houseID);
	}
}
