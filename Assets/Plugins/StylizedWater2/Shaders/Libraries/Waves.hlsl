//Stylized Water 2
//Staggart Creations (http://staggart.xyz)
//Copyright protected under Unity Asset Store EULA

struct WaveInfo
{
	float3 position;
	float3 normal;
};

float3 GerstnerOffset4(float2 xzVtx, float4 steepness, float4 amp, float4 freq, float4 speed, float4 dirAB, float4 dirCD)
{
	float3 offsets;

	float4 AB = steepness.xxyy * dirAB.xyzw * amp.xxyy;
	float4 CD = steepness.zzww * dirCD.xyzw * amp.zzww;

	float4 dotABCD = freq.xyzw * float4(dot(dirAB.xy, xzVtx), dot(dirAB.zw, xzVtx), dot(dirCD.xy, xzVtx), dot(dirCD.zw, xzVtx));

	float4 COS = cos(dotABCD + speed);
	float4 SIN = sin(dotABCD + speed);

	offsets.x = dot(COS, float4(AB.xz, CD.xz));
	offsets.z = dot(COS, float4(AB.yw, CD.yw));
	offsets.y = dot(SIN, amp); //Remap to only positive values;

	return offsets;
}

float3 GerstnerNormal4(float2 xzVtx, float4 amp, float4 freq, float4 speed, float4 dirAB, float4 dirCD)
{
	float3 nrml = float3(0, 2.0, 0);
	#if !_FLAT_SHADING || !defined(VERTEX_PASS) //Normals are per face
	float4 AB = freq.xxyy * amp.xxyy * dirAB.xyzw;
	float4 CD = freq.zzww * amp.zzww * dirCD.xyzw;

	float4 dotABCD = freq.xyzw * float4(dot(dirAB.xy, xzVtx), dot(dirAB.zw, xzVtx), dot(dirCD.xy, xzVtx), dot(dirCD.zw, xzVtx));

	float4 COS = cos(dotABCD + speed);

	nrml.x -= dot(COS, float4(AB.xz, CD.xz));
	nrml.z -= dot(COS, float4(AB.yw, CD.yw));

	nrml.xz *= _WaveNormalStr;
	nrml = normalize(nrml);
	#endif

	return nrml;
}

void Gerstner(inout float3 offs, inout float3 nrml,
	float2 position,
	float4 amplitude, float4 frequency, float4 steepness,
	float4 speed, float4 directionAB, float4 directionCD)
{
	offs += GerstnerOffset4(position, steepness, amplitude, frequency, speed, directionAB, directionCD);
	#if !_FLAT_SHADING || !defined(VERTEX_PASS) //Normals are per face
	nrml += GerstnerNormal4(position, amplitude, frequency, speed, directionAB, directionCD);
	#endif
}

#define WAVE_COUNT _WaveCount
#define MAX_WAVE_COUNT 5

WaveInfo GetWaveInfo(float2 position, float2 time)
{
	WaveInfo waves = (WaveInfo)0;

	float4 amp = float4(0.3, 0.35, 0.25, 0.25);
	float4 freq = float4(1.3, 1.35, 1.25, 1.25) * (1-_WaveDistance) * 3;
	float4 speed = float4(1.2* time.x, 1.375* time.y, 1.1* time.x, 1* time.y) ; //Pre-multiplied with time
	float4 dir1 = float4(0.3, 0.85, 0.85, 0.25) * _WaveDirection;
	float4 dir2 = float4(0.1, 0.9, -0.5, -0.5) * _WaveDirection;
	float4 steepness = float4(12.0, 12.0, 12.0, 12.0) * _WaveSteepness * lerp(1, MAX_WAVE_COUNT, 1/WAVE_COUNT);

	for (uint i = 0; i <= WAVE_COUNT; i++)
	{
		float t = 1+((float)i / (float)WAVE_COUNT);
		freq *= t;
		
		Gerstner(/*out*/ waves.position, /*out*/ waves.normal, position, amp, freq, steepness, speed, dir1, dir2);
	}

	waves.normal = normalize(waves.normal);
	//Average
	waves.position.y /= WAVE_COUNT;
	//waves.normal.xz *= WAVE_COUNT;

	return waves;
}