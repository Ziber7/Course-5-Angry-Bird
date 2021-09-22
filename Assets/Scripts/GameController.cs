using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public TrailController TrailController;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    private Bird _shotBird;
    public BoxCollider2D TapCollider;

    private bool _isGameEnded = false;

    public GameObject GameUI;
    public GameObject GameUI2;

    
    public AudioSource audioSource;
    public AudioClip clip;
    public AudioClip clip2;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for (int i = 0; i < Enemies.Count; i ++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }

    public void ChangeBird()
    {
        //TapCollider.enabled = false;

        if ( _isGameEnded)
        {
            return;
        }

        Birds.RemoveAt(0);

        if(Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }else{
            GameUI2.SetActive(true);
            audioSource.PlayOneShot(clip2, 0.5f);
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for(int i = 0; i < Enemies.Count; i++)
        {
            if(Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if(Enemies.Count == 0)
        {
            _isGameEnded = true;
            GameUI.SetActive(true);
            audioSource.PlayOneShot(clip, 0.5f);
        }
    }

    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    void OnMouseUp()
    {
        if(_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
