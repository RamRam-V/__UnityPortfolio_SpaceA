using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif



namespace Player
{
	//클라이언트 사이드에서 계산 후 서버에 전달.
	[Serializable]
	public class Input
	{
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
	}

	public class PlayerInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Input input;

		public Input _input = new Input();
		private Queue<Input> _inputs = new Queue<Input>();
		private float elapsedTime = 0;
		private ThirdPersonController_Server thirdPersonController_Server;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = false;

		public bool analogMovement;

		[SerializeField] private PlayerNetworkObjectController networkObjectController;
		private void Start()
		{
			thirdPersonController_Server = GameObject.Find("TestServerside").transform.Find("PlayerArmature").GetComponent<ThirdPersonController_Server>();
		}

		private void Update()
		{
			_inputs.Enqueue(_input);

			if (elapsedTime > 0.05f)
			{

				foreach (var i in _inputs)
				{
					thirdPersonController_Server._inputs.Enqueue(i);
				}
				thirdPersonController_Server.ProcessInput();
				_inputs.Clear();
				elapsedTime = 0;
			}
			elapsedTime += Time.deltaTime;
		}

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			_input.move = value.Get<Vector2>();
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				_input.look = value.Get<Vector2>();
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			_input.jump = value.isPressed;
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			_input.sprint = value.isPressed;
			SprintInput(value.isPressed);
		}

		public void OnPause(InputValue value)
		{
			Pause();
		}

		public void OnRightClick(InputValue value)
		{
			cursorInputForLook = value.isPressed;
			if (!value.isPressed)
				LookInput(Vector2.zero);
		}
#endif
		//start버튼 눌렀을 때만 pause 가능. 복귀할려면 브라우저 보안 정책상 반드시 마우스 클릭으로 복귀해야함. 마우스 클릭 이벤트에서 복귀 처리.
		public void Pause()
		{
			if (cursorInputForLook)
			{
				cursorInputForLook = false;
				LookInput(Vector2.zero);
			}
		}

		public void MoveInput(Vector2 newMoveDirection)
		{
			if (newMoveDirection != Vector2.zero)
			{
				networkObjectController.playerAnimationData.isMoving = true;
			}
			else
			{
				networkObjectController.playerAnimationData.isMoving = false;
			}
			input.move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			input.look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			input.jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			networkObjectController.playerAnimationData.isSprint = newSprintState;
			input.sprint = newSprintState;
		}


		private void OnApplicationFocus(bool hasFocus)
		{
		}

		//브라우저 보안 정책상 마우스 커서 잠금과 해제와 반복에 딜레이를 반드시 3초 이상을 줘야하기 때문에 커서 잠금 기능은 구현 안함.

		// public void SetCursorLock(bool newState)
		// {
		// 	Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		// 	// StartCoroutine(SetCursorStateAfterSeconds(newState, 1f));
		// }

		// IEnumerator SetCursorStateAfterSeconds(bool newState, float seconds)
		// {
		// 	yield return new WaitForSeconds(seconds);
		// 	Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		// 	yield return new WaitForSeconds(seconds);
		// }
	}

}