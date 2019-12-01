using System;

// 2019-10-10 12:55:29.935863700 UTC
namespace Offsets
{
    public static class signatures
    {
        public const Int32 anim_overlays = 0x2980;
        public const Int32 clientstate_choked_commands = 0x4D28;
        public const Int32 clientstate_delta_ticks = 0x174;
        public const Int32 clientstate_last_outgoing_command = 0x4D24;
        public const Int32 clientstate_net_channel = 0x9C;
        public const Int32 convar_name_hash_table = 0x2F0F8;
        public const Int32 dwClientState = 0x590D8C;
        public const Int32 dwClientState_GetLocalPlayer = 0x180;
        public const Int32 dwClientState_IsHLTV = 0x4D40;
        public const Int32 dwClientState_Map = 0x28C;
        public const Int32 dwClientState_MapDirectory = 0x188;
        public const Int32 dwClientState_MaxPlayer = 0x388;
        public const Int32 dwClientState_PlayerInfo = 0x52B8;
        public const Int32 dwClientState_State = 0x108;
        public const Int32 dwClientState_ViewAngles = 0x4D88;
        public const Int32 dwEntityList = 0x4D0D054;
        public const Int32 dwForceAttack = 0x313E71C;
        public const Int32 dwForceAttack2 = 0x313E728;
        public const Int32 dwForceBackward = 0x313E758;
        public const Int32 dwForceForward = 0x313E734;
        public const Int32 dwForceJump = 0x51B0708;
        public const Int32 dwForceLeft = 0x313E74C;
        public const Int32 dwForceRight = 0x313E770;
        public const Int32 dwGameDir = 0x6376F0;
        public const Int32 dwGameRulesProxy = 0x5222A4C;
        public const Int32 dwGetAllClasses = 0xD1FE0C;
        public const Int32 dwGlobalVars = 0x590A90;
        public const Int32 dwGlowObjectManager = 0x524D488;
        public const Int32 dwInput = 0x51580A0;
        public const Int32 dwInterfaceLinkList = 0x8CDF04;
        public const Int32 dwLocalPlayer = 0xCFAA2C;
        public const Int32 dwMouseEnable = 0xD00578;
        public const Int32 dwMouseEnablePtr = 0xD00548;
        public const Int32 dwPlayerResource = 0x313CA9C;
        public const Int32 dwRadarBase = 0x5141DB4;
        public const Int32 dwSensitivity = 0xD00414;
        public const Int32 dwSensitivityPtr = 0xD003E8;
        public const Int32 dwSetClanTag = 0x89A10;
        public const Int32 dwViewMatrix = 0x4CFEA84;
        public const Int32 dwWeaponTable = 0x5158B68;
        public const Int32 dwWeaponTableIndex = 0x323C;
        public const Int32 dwYawPtr = 0xD001D8;
        public const Int32 dwZoomSensitivityRatioPtr = 0xD053F8;
        public const Int32 dwbSendPackets = 0xD2F9A;
        public const Int32 dwppDirect3DDevice9 = 0xA6030;
        public const Int32 find_hud_element = 0x2B8077C0;
        public const Int32 force_update_spectator_glow = 0x393992;
        public const Int32 interface_engine_cvar = 0x3E9EC;
        public const Int32 is_c4_owner = 0x39FC20;
        public const Int32 m_bDormant = 0xED;
        public const Int32 m_flSpawnTime = 0xA360;
        public const Int32 m_pStudioHdr = 0x294C;
        public const Int32 m_pitchClassPtr = 0x5142060;
        public const Int32 m_yawClassPtr = 0xD001D8;
        public const Int32 model_ambient_min = 0x593DAC;
        public const Int32 set_abs_angles = 0x1CAE70;
        public const Int32 set_abs_origin = 0x1CACB0;
    }
}

/*Help
 * DT_WeaponCSBase -> m_fAccuracyPenalty: _________ 0x000032C0
DT_BaseAnimating -> m_nForceBone: ______________ 0x0000267C
DT_BaseCombatWeapon -> m_iState: _______________ 0x000031F8
DT_BaseCombatWeapon -> m_iClip1: _______________ 0x00003204
DT_BaseCombatWeapon -> m_flNextPrimaryAttack: __ 0x000031D8
DT_BaseCombatWeapon -> m_bCanReload: ___________ 0x00003245
DT_BaseCombatWeapon -> m_iPrimaryAmmoType: _____ 0x000031FC
DT_BaseCombatWeapon -> m_iWeaponID: ____________ 0x000032EC
DT_WeaponCSBaseGun -> m_zoomLevel: _____________ 0x00003350
DT_BaseEntity -> m_bSpotted: ___________________ 0x00000939
DT_BaseEntity -> m_bSpottedByMask: _____________ 0x0000097C
DT_BaseEntity -> m_hOwnerEntity: _______________ 0x00000148
DT_BaseEntity -> m_vecOrigin: __________________ 0x00000134
DT_BaseEntity -> m_iTeamNum: ___________________ 0x000000F0
DT_CSPlayer -> m_flFlashMaxAlpha: ______________ 0x0000A304
DT_CSPlayer -> m_flFlashDuration: ______________ 0x0000A308
DT_CSPlayer -> m_iGlowIndex: ___________________ 0x0000A320
DT_CSPlayer -> m_angEyeAngles: _________________ 0x0000AA08
DT_CSPlayer -> m_iAccount: _____________________ 0x0000A9F8
DT_CSPlayer -> m_ArmorValue: ___________________ 0x0000AA04
DT_CSPlayer -> m_bGunGameImmunity: _____________ 0x000038B0
DT_CSPlayer -> m_iShotsFired: __________________ 0x0000A2C0
DT_CSPlayerResource -> CSPlayerResource: _______ 0x02F07524
DT_CSPlayerResource -> m_iCompetitiveRanking: __ 0x00001A44
DT_CSPlayerResource -> m_iCompetitiveWins: _____ 0x00001B48
DT_CSPlayerResource -> m_iKills: _______________ 0x00000BE8
DT_CSPlayerResource -> m_iAssists: _____________ 0x00000CEC
DT_CSPlayerResource -> m_iDeaths: ______________ 0x00000DF0
DT_CSPlayerResource -> m_iPing: ________________ 0x00000AE4
DT_CSPlayerResource -> m_iScore: _______________ 0x00001940
DT_CSPlayerResource -> m_szClan: _______________ 0x00004120
DT_BasePlayer -> m_lifeState: __________________ 0x0000025B
DT_BasePlayer -> m_fFlags: _____________________ 0x00000100
DT_BasePlayer -> m_iHealth: ____________________ 0x000000FC
DT_BasePlayer -> m_hLastWeapon: ________________ 0x000032F8
DT_BasePlayer -> m_hMyWeapons: _________________ 0x00002DE8
DT_BasePlayer -> m_hActiveWeapon: ______________ 0x00002EE8
DT_BasePlayer -> m_Local: ______________________ 0x00002FAC
DT_BasePlayer -> m_vecViewOffset[0]: ___________ 0x00000104
DT_BasePlayer -> m_nTickBase: __________________ 0x00003424
DT_BasePlayer -> m_vecVelocity[0]: _____________ 0x00000110
DT_BasePlayer -> m_szLastPlaceName: ____________ 0x000035A8
DT_Local -> m_vecPunch: ________________________ 0x0000301C
DT_Local -> m_iCrossHairID: ____________________ 0x0000AA70
BaseEntity -> m_bDormant: ______________________ 0x000000E9
BaseEntity -> m_dwModel: _______________________ 0x0000006C
BaseEntity -> m_dwIndex: _______________________ 0x00000064
BaseEntity -> m_dwBoneMatrix: __________________ 0x00002698
BaseEntity -> m_bMoveType: _____________________ 0x00000258
ClientState -> m_dwClientState: ________________ 0x005CA534
ClientState -> m_dwLocalPlayerIndex: ___________ 0x00000178
ClientState -> m_dwInGame: _____________________ 0x00000100
ClientState -> m_dwMaxPlayer: __________________ 0x00000308
ClientState -> m_dwMapDirectory: _______________ 0x00000180
ClientState -> m_dwMapname: ____________________ 0x00000284
ClientState -> m_dwPlayerInfo: _________________ 0x0000523C
ClientState -> m_dwViewAngles: _________________ 0x00004D0C
EngineRender -> m_dwViewMatrix: ________________ 0x04ABAD54
EngineRender -> m_dwEnginePosition: ____________ 0x0067928C
RadarBase -> m_dwRadarBase: ____________________ 0x04EFDF0C
RadarBase -> m_dwRadarBasePointer: _____________ 0x00000050
LocalPlayer -> m_dwLocalPlayer: ________________ 0x00AA6818
EntityList -> m_dwEntityList: __________________ 0x04AC91B4
WeaponTable -> m_dwWeaponTable: ________________ 0x04F1030C
WeaponTable -> m_dwWeaponTableIndex: ___________ 0x00003270
Extra -> m_dwInput: ____________________________ 0x04F13C80
Extra -> m_dwGlobalVars: _______________________ 0x004E0474
Extra -> m_dwGlowObject: _______________________ 0x04FE39B4
Extra -> m_dwForceJump: ________________________ 0x04F5FCE4
Extra -> m_dwForceAttack: ______________________ 0x02F09300
Extra -> m_dwSensitivity: ______________________ 0x00000000
Extra -> m_dwMouseEnable: ______________________ 0x00000000

    */