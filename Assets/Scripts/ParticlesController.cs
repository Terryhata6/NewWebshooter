using UnityEngine;

public class ParticlesController : MonoBehaviour
{
	public ParticleSystem SmallExplosinParticles;
	private ParticleSystem _smallExplosinObject;

	private void Awake()
	{
		_smallExplosinObject = Instantiate(SmallExplosinParticles, new Vector3(0, 0, -20f), Quaternion.identity);
	}

	public void MakeSmallExplosion(Vector3 position) 
	{
		_smallExplosinObject.transform.position = position;
		_smallExplosinObject.Play();
	}
}
