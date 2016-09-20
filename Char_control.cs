using UnityEngine;
using System.Collections;
public class Char_control : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector3 acc;
    public float velocity = 0f;
    tk2dSpriteAnimator animator;   
    bool life = true;   
    public GameObject objtemp;
    public Satan_patern Satan;
    public GameObject slash;
    public GameObject smash;
    bool effect_check = false;

    public AudioClip hitSound = null;
    public AudioClip crtSound = null;
    float dis = 0f;
    tk2dSpriteAnimationClip Att_Clip;

    // Use this for initialization
    void Start()
    {        
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<tk2dSpriteAnimator>();
        Att_Clip = animator.GetClipByName("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        dis = this.transform.position.x -  Satan.transform.position.x;      


        if (life)
        {
           Run(-(velocity-0.03f));

            if (Input.GetKey("space"))
                Run(velocity);   

            if (Input.GetMouseButton(0))
            {
                animator.Play("Attack");                
                if (dis < 2.25 && 1.45 < dis)
                {
                    StartCoroutine(Slash_manage(slash,hitSound));                  
                }
                if(dis <= 1.45)
                {
                    StartCoroutine(Smash_manage(smash,crtSound));
                }
              
            }
            else
               animator.Play("Run");
        }
    }

    void Run(float x)
    {
        acc.Set(x, 0, 0);
        rigid.MovePosition(transform.position + acc);
    }

    void OnTriggerEnter2D(Collider2D term)
    {
        if (term.name == "end" || term.name == "Satan" || term.name == "dogani(Clone)"|| term.name == "XP_EWOrangeBlastA_1A_0000 1(Clone)")
        {
           
            life = false;
            animator.Play("Die");
            GameObject retryob = objtemp;
            retryob.transform.position = new Vector3(6, 2, 0);
        }
    }

    IEnumerator Smash_manage(GameObject eff, AudioClip source)
    {
        if (effect_check)
        {
            effect_check = false;
            yield return new WaitForSeconds(0.4f);
        }

                
        if (animator.CurrentFrame == 5)
        {
            Satan.life -= 7f;           
            Instantiate(eff, transform.position, Quaternion.identity);
            PlaySound(source);
            effect_check = true;
        }

        yield return new WaitForSeconds(0f);

    }

    IEnumerator Slash_manage(GameObject eff, AudioClip source)
    {
        if (effect_check)
        {
            effect_check = false;
            yield return new WaitForSeconds(0.4f);
        }


        if (animator.CurrentFrame == 5)
        {
            Satan.life -= 7f;
            Instantiate(eff, transform.position + new Vector3(-1.5f,0,0), Quaternion.identity);
            PlaySound(source);
            effect_check = true;
        }

        yield return new WaitForSeconds(0f);

    }


    void PlaySound(AudioClip source)
    {
        if (GetComponent<AudioSource>() && source)
        {
            GetComponent<AudioSource>().PlayOneShot(source);
        }
    }
}


