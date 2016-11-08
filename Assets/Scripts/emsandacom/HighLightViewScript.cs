using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.UI;

namespace View
{
	public class HighLightViewScript : MonoBehaviour {
		public static HighLightViewScript Instance;
		public GameObject[] ListPlayer;
		public Image HighLight;
        public GameObject container;

		private Coroutine coroutine = null;

		void Start()
		{
			Instance = this;
		}

		public void HighLightTo(GameObject target)
		{
			 StartCoroutine(HighLightToThread(target));
		}
		IEnumerator HighLightToThread(GameObject target)
		{

            Vector3 target_position = target.transform.localPosition;
            //Debug.LogError("Target " + target_position);
            float angle = Vector3.Angle(target_position, new Vector3(0, -152, 0));
            //float angle = Vector3.Angle(container.transform.localPosition, target_position);
            //Debug.LogError("angle : " + angle);

            if (target_position.x < 0)
			{
				angle *= -1;
			}
            //HighLight.DOFade(0.01f, 1f);
            container.transform.DOLocalRotate (new Vector3 (0, 0, angle), .3f);
			//float distance = Mathf.Sqrt(target_position.x * target_position.x + target_position.y * target_position.y) - 50f;
			//HighLight.rectTransform.sizeDelta = new Vector3(83, Convert.ToInt32(distance));
			yield return null;
			/*
			TweenAlpha.Begin(HighLight.gameObject, 0.2f, 0f);
			yield return new WaitForSeconds(0.2f);
			if(isdestroyed)
				yield break;
			TweenRotation tween = TweenRotation.Begin(HighLight.gameObject, 0.1f, Quaternion.identity);
			if(tween.from.z > 180)
				tween.from = new Vector3(0, 0, 180 - tween.from.z);
			tween.to = new Vector3(0, 0, angle);
			yield return new WaitForSeconds(0.1f);
			if(isdestroyed)
				yield break;
			TweenAlpha.Begin(HighLight.gameObject, 0.2f, 1f);
			
			float distance = Mathf.Sqrt(target_position.x * target_position.x + target_position.y * target_position.y) - 50f;
			HighLight.height = Convert.ToInt32(distance);*/
		}


		public void DisableHighLight()
		{
			HighLight.gameObject.SetActive(false);
		}

        int index = 0;
		public void HighLIghtTestButtonClick()
		{
			HighLightTo(ListPlayer[index]);
            index++;
            if (index > ListPlayer.Length - 1)
                index = 0;
        }

		void OnDestroy()
		{
			Instance = null;
		}
	}
}