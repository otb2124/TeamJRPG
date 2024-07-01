#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler TextureSampler : register(s0);

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float2 center = float2(0.5, 0.5); // Center of the texture
    float distance = distance(coords, center);

    // If the distance from the center is greater than 0.5 (the radius), discard the pixel
    if (distance > 0.5)
    {
        discard;
    }

    return tex2D(TextureSampler, coords);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile PS_SHADERMODEL PixelShaderFunction();
    }
}
