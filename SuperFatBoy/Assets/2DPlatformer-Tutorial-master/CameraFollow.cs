using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CameraFollow : MonoBehaviour {

    BoxCollider2D target;
	public float verticalOffset;
	public float verticalSmoothTime;
    public float horisontalSmoothTime;
    public float cameraMaxScale = 9f;
    public float cameraMinScale = 5f;
    public float cameraScaleSpeed = 5f;
	public Vector2 focusAreaSize;

    public Rect limit;

    Camera camera;
    FocusArea focusArea;
    Rect camRect =new Rect();

	float currentLookAheadX;
	float targetLookAheadX;
	float lookAheadDirX;
	float smoothLookVelocityX;
	float smoothVelocityY;

	bool lookAheadStopped;
    bool isScaleing;

    void Awake()
    {
        camera = GetComponent<Camera>();
        PlayerSpawner.spawnEvent += SetTarget;
    }
    void Update()
    {
        float orthographicSize = camera.orthographicSize;

        camRect.min = new Vector2(transform.position.x - camera.aspect * orthographicSize, transform.position.y - orthographicSize);
        camRect.max = new Vector2(transform.position.x + camera.aspect * orthographicSize, transform.position.y + orthographicSize);

        if (CrossPlatformInputManager.GetButton("Scale"))
        {
            isScaleing = true;

            if (orthographicSize <= cameraMaxScale)
                orthographicSize += cameraScaleSpeed * Time.deltaTime;

            transform.position = CameraShift(transform.position);
        }
        else
        {
            isScaleing = false;//сделать нормально. Камера должна плавно возвращатся обратно.
            if(orthographicSize >= cameraMinScale)
            orthographicSize -=cameraScaleSpeed * Time.deltaTime;
        }
        camera.orthographicSize = orthographicSize;
    }
    Vector3 CameraShift( Vector3 currPos)
    {
        Vector3 campos = currPos;
        if (camRect.xMin < limit.xMin)
        {
            campos.x += limit.xMin - camRect.xMin;
        }
        if (camRect.xMax > limit.xMax)
        {
            campos.x -= camRect.xMax - limit.xMax;
        }
        if (camRect.yMin < limit.yMin)
        {
            campos.y += limit.yMin - camRect.yMin;
        }
        if (camRect.yMax > limit.yMax)
        {
            campos.y -= camRect.yMax - limit.yMax;
        }

        return campos;
    }
	void LateUpdate() {
        if (target == null || isScaleing) //будем пулить и запоминать игрока, по этому должна быть проверка на enable
            return;

        focusArea.Update(target.bounds, camera.orthographicSize * camera.aspect, camera.orthographicSize);

		Vector2 focusPosition = focusArea.centre + Vector2.up * verticalOffset;
       
		focusPosition.x = Mathf.SmoothDamp(transform.position.x, focusPosition.x, ref smoothLookVelocityX, horisontalSmoothTime);
		focusPosition.y = Mathf.SmoothDamp (transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
        
        //focusPosition += Vector2.right * currentLookAheadX;
        transform.position = (Vector3)focusPosition + Vector3.forward * -10;
	}
    public void SetTarget(Transform controller)
    {
        //Ошибка, когда спаунится игрок камера тут же прыгает на него. обходя ограничение по лимиту.
        target = controller.GetComponent<BoxCollider2D>();
        focusArea = new FocusArea(target.bounds, focusAreaSize, limit);
    }
	void OnDrawGizmos() {
		Gizmos.color = new Color (1, 0, 0, .5f);
		Gizmos.DrawWireCube (focusArea.centre, focusAreaSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(limit.center, limit.size);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(camRect.center, camRect.size);
	}
    void OnDisable()
    {
        PlayerSpawner.spawnEvent -= SetTarget;
    }
	struct FocusArea {
		public Vector2 centre;
		public Vector2 velocity;

        Rect limit;
		float left,right;
		float top,bottom;

        //float hulfScreenWidth, hulfScreenHeight;

        public FocusArea(Bounds targetBounds, Vector2 size,Rect _limit)
        {
            limit = _limit;
            //hulfScreenWidth = _hulfScreenWidth;
            //hulfScreenHeight = _hulfScreenHeight;

			left = targetBounds.center.x - size.x/2;
			right = targetBounds.center.x + size.x/2;
			bottom = targetBounds.min.y;
			top = targetBounds.min.y + size.y;

			velocity = Vector2.zero;
			centre = new Vector2((left+right)/2,(top +bottom)/2);
		}

        public void Update(Bounds targetBounds, float hulfScreenWidth, float hulfScreenHeight)
        {

			float shiftX = 0;
            if (targetBounds.min.x < left && targetBounds.min.x >=limit.xMin+hulfScreenWidth)
            {
				shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right && targetBounds.max.x <= limit.xMax - hulfScreenWidth)
            {
				shiftX = targetBounds.max.x - right;
			}
			left += shiftX;
			right += shiftX;

			float shiftY = 0;
            if (targetBounds.min.y < bottom && targetBounds.min.y >= limit.yMin + hulfScreenHeight)
            {
				shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top && targetBounds.max.y <= limit.yMax - hulfScreenHeight)
            {
				shiftY = targetBounds.max.y - top;
			}
			top += shiftY;
			bottom += shiftY;
			centre = new Vector2((left+right)/2,(top +bottom)/2);
			velocity = new Vector2 (shiftX, shiftY);
		}
	}

}
