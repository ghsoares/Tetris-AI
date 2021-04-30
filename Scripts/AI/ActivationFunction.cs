using System;

namespace MachineLearning
{
    public struct ActivationFunction
    {
        public Func<float,float> activation {get; set;}
        public Func<float,float> derivative {get; set;}

        public ActivationFunction(Func<float,float> activation, Func<float,float> derivative) {
            this.activation = activation;
            this.derivative = derivative;
        }

        public float Activate(float val) {
            return activation.Invoke(val);
        }

        public float Derivate(float val) {
            return derivative.Invoke(val);
        }
    }
}