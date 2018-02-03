using UnityEngine;


    public class CameraRayCaster : MonoBehaviour
    {
        //private MyCursor myCursor;
        public Layer[] layerPriorities = { Layer.Enemy, Layer.NPC, Layer.PickUp, Layer.Walkable };

        private float maxRayDepth = 100f;
        private Camera viewCamera;
        private int layerDefault = -1;
        
        private RaycastHit _hit;

        //to get the information from the hit
        public RaycastHit hit
        {
            get { return _hit; }
        }

        private Layer _LayerHit;

        //to get which layer the ray cast has hit
        public Layer layerHit
        {
            get { return _LayerHit; }
        }

        private void Awake()
        {
            //myCursor = GetComponent<MyCursor>();
        }

        // Use this for initialization
        private void Start()
        {
            viewCamera = Camera.main;
            
        }

        // Update is called once per frame
        private void Update()
        {
            //look for and return priority layer hit
            foreach (Layer layer in layerPriorities)
            {
                var hit = RaycastForLayer(layer);
                if (hit.HasValue)
                {
                    _hit = hit.Value;
                    _LayerHit = layer;
                    //myCursor.CursorUpdate((int)_LayerHit);
                    return;
                }
            }

            //if not will return background hit
            _hit.distance = maxRayDepth;
            _LayerHit = Layer.RaycastEnd;
        }

        //the ? will permit to return a null
        private RaycastHit? RaycastForLayer(Layer layer)
        {
            int layerMask = 1 << (int)layer;

            //use has out
            RaycastHit hit;
            Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(viewCamera.transform.position, ray.direction, Color.blue);
            bool hasHit = Physics.Raycast(ray, out hit, maxRayDepth, layerMask);

            if (hasHit)
            {
                return hit;
            }
            return null;
        }
    }
