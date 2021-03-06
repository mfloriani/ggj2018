using UnityEngine;

public class Player : MonoBehaviour {

    private Countdown countdown;

    float timeRemaining = 0f;
    public float force;

    private bool MoveRight, MoveLeft, MoveUp, MoveDown;

    public bool disguised = false;
    public float velocityMultiplier = 0.5f;
    public float runMultiplier = 0.5f;
    public float radius = 0.5f;

    private SoldierAnimatorController animatorController;

    // Use this for initialization
    void Awake() {
        MoveRight = true;
        MoveLeft = false;
        MoveUp = false;
        MoveDown = false;
    }

    void Start() {
        animatorController = GetComponent<SoldierAnimatorController>();
    }

    // Update is called once per frame
    void Update() {
        // Funcao de Movimentacao
        Move();

        // Verifica se há algum objeto com ação proximo
        CheckForActionItem();
    }

    void Move() {
        Vector3 movement = new Vector3(Input.GetAxisRaw("X360_LStickX01"), 0, Input.GetAxisRaw("X360_LStickY01"));
        //float run = Mathf.Abs(Input.GetAxisRaw("X360_RightTrigger01"));
        float run = Input.GetButton("X360_X01") ? 1 : 0;

        Vector3 dir = transform.position + movement;
        transform.LookAt(dir);
        
        run *= runMultiplier;
        float multiplierResult = velocityMultiplier + run;
        transform.position += (  multiplierResult * movement * Time.deltaTime);

        animatorController.setForward(movement.magnitude);
    }





    // Update is called once per frame
    void CheckForActionItem ()
    {
        if (Input.GetButtonDown("X360_A01"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider other in colliders){
                if (other.gameObject.tag == "Disguise" && disguised == false)
                {
                    FeedbackMessage.getInstance().AddMessage("Voce pegou um disfarce", 5);

                    if (!disguised) {
                        disguised = true;
                    }
                }
                else if (other.gameObject.tag == "Info")
                {
                    Config.getInstance().UpdateCollectedInfos();
                    FeedbackMessage.getInstance().AddMessage("Voce pegou uma informação", 5);
                    Destroy(other.gameObject);

                }
            }
        }
    }
}
