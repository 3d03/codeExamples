using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderReadAndRotateServ : MonoBehaviour {

	public int ServoIndex;
	public int FreezedXrot;
	public int FreezedYrot;
	public int FreezedZrot;

	public int offset;
	public bool Inversed;

	public enum RotationAxisEnum { x, y, z };
	public RotationAxisEnum RotationAxis;

	public Slider LinkedSlider;
	public GameObject LinkedRotator;
	
	void Start () {
		
	}
	
	
	public void RotateBySliderChange () {

		int def = JSONReader.Instance.Defaults[ServoIndex - 1];

		if (RotationAxis == RotationAxisEnum.x)
		{
			if (Inversed)
			
			{
				var rot = LinkedRotator.transform.localRotation.eulerAngles; //get the angles
				rot.Set(def - (LinkedSlider.value - def) + offset, FreezedYrot, FreezedZrot); //set the angles
				LinkedRotator.transform.localRotation = Quaternion.Euler(rot); //update the transform
			}
			else
			
			{
				var rot = LinkedRotator.transform.localRotation.eulerAngles; //get the angles
				rot.Set(LinkedSlider.value + offset, FreezedYrot, FreezedZrot); //set the angles
				LinkedRotator.transform.localRotation = Quaternion.Euler(rot); //update the transform
			}
		}


		if (RotationAxis == RotationAxisEnum.y)
		{


			if (Inversed)
			
			{
				
				var rot = LinkedRotator.transform.localRotation.eulerAngles; //get the angles
				rot.Set(FreezedXrot, def - (LinkedSlider.value - def) + offset, FreezedZrot); //set the angles
				LinkedRotator.transform.localRotation = Quaternion.Euler(rot); //update the transform
			}
			else
			{
			
				var rot = LinkedRotator.transform.localRotation.eulerAngles; //get the angles
				rot.Set(FreezedXrot, LinkedSlider.value + offset, FreezedZrot); //set the angles
				LinkedRotator.transform.localRotation = Quaternion.Euler(rot); //update the transform
			}
		}



		if (RotationAxis == RotationAxisEnum.z)
		{

			if (Inversed)
			{
			
				var rot = LinkedRotator.transform.localRotation.eulerAngles; //get the angles
				rot.Set(FreezedXrot, FreezedYrot, def - (LinkedSlider.value - def) + offset); //set the angles
				LinkedRotator.transform.localRotation = Quaternion.Euler(rot); //update the transform
			}
			else
			{
			
				var rot = LinkedRotator.transform.localRotation.eulerAngles; //get the angles
				rot.Set(FreezedXrot, FreezedYrot, LinkedSlider.value + offset); //set the angles
				LinkedRotator.transform.localRotation = Quaternion.Euler(rot); //update the transform
			}

			
		}



		LinkedSlider.gameObject.transform.parent.gameObject.GetComponentInChildren<Text>().text = LinkedSlider.value.ToString();
		Debug.Log(LinkedRotator.transform.localRotation.eulerAngles);

 
	}
}
