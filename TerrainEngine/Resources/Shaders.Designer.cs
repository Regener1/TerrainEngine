﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TerrainEngine.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Shaders {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Shaders() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TerrainEngine.Resources.Shaders", typeof(Shaders).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #version 420 core
        ///
        ///in vec3 colour;
        ///in vec2 pass_textureCoordinates;
        ///
        ///in vec3 pass_brushPosition;
        ///in float pass_brushRadius;
        ///in float pass_terrainSize;
        ///
        ///out vec4 out_Color;
        ///
        ///uniform sampler2D modelTexture;
        /////uniform sampler2D rTexture;
        /////uniform sampler2D gTexture;
        /////uniform sampler2D bTexture;
        /////uniform sampler2D blendMap;
        ///
        ///bool inCircle(float cx, float cz, float x, float z, float radius)
        ///{
        ///	return (x - cx) * (x - cx) + (z - cz) * (z - cz) &lt; radius * radius;
        ///}
        ///
        ///void main(void){
        ///
        /////	v [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string terrainFragmentShader {
            get {
                return ResourceManager.GetString("terrainFragmentShader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #version 420 core
        ///
        ///in vec3 position;
        ///in vec2 textureCoordinates;
        ///
        ///out vec3 colour;
        ///out vec2 pass_textureCoordinates;
        ///
        ///out vec3 pass_brushPosition;
        ///out float pass_brushRadius;
        ///out float pass_terrainSize;
        ///
        ///uniform mat4 transformationMatrix;
        ///uniform mat4 projectionMatrix;
        ///uniform mat4 viewMatrix;
        ///
        ///uniform vec3 brushPosition;
        ///uniform float brushRadius;
        ///uniform float terrainSize;
        ///
        ///void main(void){
        ///
        ///	gl_Position = projectionMatrix * viewMatrix * transformationMatrix * vec4(position,1.0);
        ///	
        /// </summary>
        internal static string terrainVertexShader {
            get {
                return ResourceManager.GetString("terrainVertexShader", resourceCulture);
            }
        }
    }
}