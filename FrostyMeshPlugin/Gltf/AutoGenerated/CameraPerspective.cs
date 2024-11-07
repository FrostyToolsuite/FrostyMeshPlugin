//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FrostyMeshPlugin.Gltf.AutoGenerated {
    using System.Linq;
    using System.Runtime.Serialization;
    
    
    public class CameraPerspective {
        
        /// <summary>
        /// Backing field for AspectRatio.
        /// </summary>
        private System.Nullable<float> m_aspectRatio;
        
        /// <summary>
        /// Backing field for Yfov.
        /// </summary>
        private float m_yfov;
        
        /// <summary>
        /// Backing field for Zfar.
        /// </summary>
        private System.Nullable<float> m_zfar;
        
        /// <summary>
        /// Backing field for Znear.
        /// </summary>
        private float m_znear;
        
        /// <summary>
        /// Backing field for Extensions.
        /// </summary>
        private System.Collections.Generic.Dictionary<string, object> m_extensions;
        
        /// <summary>
        /// Backing field for Extras.
        /// </summary>
        private Extras m_extras;
        
        /// <summary>
        /// The floating-point aspect ratio of the field of view.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("aspectRatio")]
        public System.Nullable<float> AspectRatio {
            get {
                return this.m_aspectRatio;
            }
            set {
                if ((value <= 0D)) {
                    throw new System.ArgumentOutOfRangeException("AspectRatio", value, "Expected value to be greater than 0");
                }
                this.m_aspectRatio = value;
            }
        }
        
        /// <summary>
        /// The floating-point vertical field of view in radians. This value **SHOULD** be less than π.
        /// </summary>
        [System.Text.Json.Serialization.JsonRequiredAttribute()]
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("yfov")]
        public float Yfov {
            get {
                return this.m_yfov;
            }
            set {
                if ((value <= 0D)) {
                    throw new System.ArgumentOutOfRangeException("Yfov", value, "Expected value to be greater than 0");
                }
                this.m_yfov = value;
            }
        }
        
        /// <summary>
        /// The floating-point distance to the far clipping plane.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("zfar")]
        public System.Nullable<float> Zfar {
            get {
                return this.m_zfar;
            }
            set {
                if ((value <= 0D)) {
                    throw new System.ArgumentOutOfRangeException("Zfar", value, "Expected value to be greater than 0");
                }
                this.m_zfar = value;
            }
        }
        
        /// <summary>
        /// The floating-point distance to the near clipping plane.
        /// </summary>
        [System.Text.Json.Serialization.JsonRequiredAttribute()]
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("znear")]
        public float Znear {
            get {
                return this.m_znear;
            }
            set {
                if ((value <= 0D)) {
                    throw new System.ArgumentOutOfRangeException("Znear", value, "Expected value to be greater than 0");
                }
                this.m_znear = value;
            }
        }
        
        /// <summary>
        /// JSON object with extension-specific objects.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("extensions")]
        public System.Collections.Generic.Dictionary<string, object> Extensions {
            get {
                return this.m_extensions;
            }
            set {
                this.m_extensions = value;
            }
        }
        
        /// <summary>
        /// Application-specific data.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("extras")]
        public Extras Extras {
            get {
                return this.m_extras;
            }
            set {
                this.m_extras = value;
            }
        }
        
        public bool ShouldSerializeAspectRatio() {
            return ((m_aspectRatio == null) 
                        == false);
        }
        
        public bool ShouldSerializeZfar() {
            return ((m_zfar == null) 
                        == false);
        }
        
        public bool ShouldSerializeExtensions() {
            return ((m_extensions == null) 
                        == false);
        }
        
        public bool ShouldSerializeExtras() {
            return ((m_extras == null) 
                        == false);
        }
    }
}
