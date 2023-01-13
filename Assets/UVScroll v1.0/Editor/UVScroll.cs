using UnityEngine;
using UnityEditor;

public class UVScroll : ShaderGUI {
	
	MaterialEditor editor;
	MaterialProperty[] properties;
	
	public override void OnGUI (
		MaterialEditor editor, MaterialProperty[] properties
	) {
		this.editor = editor;
		this.properties = properties;
		DoMain();
	}
	
	MaterialProperty FindProperty (string name) {
		return FindProperty(name, properties);
	}
	
	static GUIContent staticLabel = new GUIContent();
	
	static GUIContent MakeLabel (MaterialProperty property, string tooltip = null) {
		staticLabel.text = property.displayName;
		staticLabel.tooltip = tooltip;
		return staticLabel;
	}
	
	void DoMain() {
		GUILayout.Label("Thanks for using my shader~ <3");
		GUILayout.Label("");
		DoTexture();
		DoScrollingTexture();
		DoOptions();
		GUILayout.Label("Questions? Suggestions? Need help? Contact me on discord! Cdrsan#8433");
	}
	
	void DoTexture() {
		MaterialProperty _Texture = FindProperty("_Texture");
		MaterialProperty _Color0 = FindProperty("_Color0");
		editor.TexturePropertySingleLine(
				MakeLabel(_Texture, "Texture (RGB)"), _Texture, FindProperty("_Color0")
			);
		EditorGUI.indentLevel += 1;
		editor.TextureScaleOffsetProperty(_Texture);
		EditorGUI.indentLevel -= 1;
		GUILayout.Label("");
	}
	
	void DoScrollingTexture() {
		MaterialProperty _ScrollingTexture = FindProperty("_ScrollingTexture");
		MaterialProperty _Color1 = FindProperty("_Color1");
		MaterialProperty _ScrollX = FindProperty("_ScrollX");
		MaterialProperty _ScrollY = FindProperty("_ScrollY");
		editor.TexturePropertySingleLine(
				MakeLabel(_ScrollingTexture, "Texture (RGB)"), _ScrollingTexture, FindProperty("_Color1")
			);
		EditorGUI.indentLevel += 1;
		editor.TextureScaleOffsetProperty(_ScrollingTexture);
		if (_ScrollingTexture.textureValue) {
			EditorGUI.indentLevel += 1;
			editor.ShaderProperty(_ScrollX, MakeLabel(_ScrollX, "Changes the scroll speed on the X axis"));
			editor.ShaderProperty(_ScrollY, MakeLabel(_ScrollY, "Changes the scroll speed on the Y axis"));
		}
		EditorGUI.indentLevel -= 2;
		GUILayout.Label("");
	}
	
	void DoOptions() {
		MaterialProperty _Blend = FindProperty("_Blend");
		MaterialProperty _Alpha = FindProperty("_Alpha");
		MaterialProperty _Emission = FindProperty("_Emission");
		editor.ShaderProperty(_Blend, MakeLabel(_Blend, "Blends between the two textures"));
		editor.ShaderProperty(_Alpha, MakeLabel(_Alpha, "Controls overall transparency"));
		editor.ShaderProperty(_Emission, MakeLabel(_Emission, "Multiplies color output into HDR range for bloom worlds"));
		GUILayout.Label("");
	}
}