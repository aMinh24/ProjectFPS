Script:
 - Data
	+ achivementList ok
	+ CONST ok
	+ Enum
	+ GlobalConfig
	+ GunInfo ok
	+ missionList ok
	+ playerData ok
 - EnemyAI
	AIState: attack idle attack
	FSM:
	 	+AIstate ok
		+AIstatemachine
	Aiagent
	Aihealth
	ailocomotion ok
	aiweapon
	aiweaponIK
 - General:
	health
	hitbox ok
	ragdoll
 - Listener
	_animationevent ok
	listenergroup
	listenEvent ok
	weaponAnim ok
	
 - Manager
	Audio
	Base
	cam
	data
	game ok
	listener
	mission
	UI
 - Player
	activeweapon
	characteraiming
	characterlocomotion
	playerAnim ok
	playerHealth 
 - UI
	Base:	notify, overlap,popup,screen ok
		UIElement
	Notify: LoadingGame
		NotifyMission ok
	Overlap: confirmBox ok
		 overlapfade
	Popup: GameMenu, MissionPanel
	Screen: Defeat,killdisplay,Mainmenu,victory
	
	Audiobutton,tabmission ok
	missionrow
 - Weapon: bullet,crosshair,objectpool,weaponevent,raycast,recoil,reload