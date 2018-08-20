Shader "Lines/VertexColored" {
	SubShader {
		Pass {
			ZWrite Off Cull Off Fog { Mode Off }
			Blend SrcAlpha OneMinusSrcAlpha
			BindChannels {
				Bind "vertex", vertex
				Bind "color", color
			}
		}
	}
}
