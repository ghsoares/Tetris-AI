using System;
using Godot;

namespace MachineLearning
{
    public class NeuralNetwork
    {
        public int numInputNodes { get; private set; }
        public int numHiddenNodes { get; private set; }
        public int numOutputNodes { get; private set; }

        public Matrix biasInputHidden { get; private set; }
        public Matrix biasHiddenOutput { get; private set; }

        public Matrix weightInputHidden { get; private set; }
        public Matrix weightHiddenOutput { get; private set; }

        public float learningRate {get; private set;}

        public ActivationFunction activationFunctionInputHidden { get; private set; }
        public ActivationFunction activationFunctionHiddenOutput { get; private set; }

        public NeuralNetwork(int numInputNodes, int numHiddenNodes, int numOutputNodes, float learningRate = .1f)
        {
            this.numInputNodes = numInputNodes;
            this.numHiddenNodes = numHiddenNodes;
            this.numOutputNodes = numOutputNodes;

            this.biasInputHidden = new Matrix(numHiddenNodes, 1);
            this.biasHiddenOutput = new Matrix(numOutputNodes, 1);

            this.weightInputHidden = new Matrix(numHiddenNodes, numInputNodes);
            this.weightHiddenOutput = new Matrix(numOutputNodes, numHiddenNodes);

            this.learningRate = learningRate;

            this.activationFunctionInputHidden = new ActivationFunction(
                (x) => 1f / (1f + Mathf.Exp(-x)),
                (x) => x * (1f - x)
            );
            this.activationFunctionHiddenOutput = new ActivationFunction(
                (x) => 1f / (1f + Mathf.Exp(-x)),
                (x) => x * (1f - x)
            );

            this.Randomize();
        }
    
        public NeuralNetwork(NeuralNetwork copy) {
            this.numInputNodes = copy.numInputNodes;
            this.numHiddenNodes = copy.numHiddenNodes;
            this.numOutputNodes = copy.numOutputNodes;

            this.biasInputHidden = new Matrix(copy.biasInputHidden);
            this.biasHiddenOutput = new Matrix(copy.biasHiddenOutput);

            this.weightInputHidden = new Matrix(copy.weightInputHidden);
            this.weightHiddenOutput = new Matrix(copy.weightHiddenOutput);

            this.learningRate = copy.learningRate;
            this.activationFunctionInputHidden = copy.activationFunctionInputHidden;
            this.activationFunctionHiddenOutput = copy.activationFunctionHiddenOutput;
        }

        public void Train(Matrix input, Matrix target) {
            // Feed Forward
            Matrix hidden = this.weightInputHidden * input;
            hidden += this.biasInputHidden;
            hidden.Map((el, i, j) => this.activationFunctionInputHidden.Activate(el));

            Matrix output = this.weightHiddenOutput * hidden;
            output += this.biasHiddenOutput;
            output.Map((el, i, j) => this.activationFunctionHiddenOutput.Activate(el));

            // Back Propagation
            // Output -> Hidden
            Matrix outError = target - output;
            Matrix dOut = Matrix.Map(
                output, (el, i, j) => this.activationFunctionHiddenOutput.Derivate(el)
            );
            Matrix hiddenT = hidden.Transposed();

            Matrix outGrad = Matrix.Hadamard(dOut, outError) * this.learningRate;
            Matrix wHidOutD = outGrad * hiddenT;

            this.biasHiddenOutput += outGrad;
            this.weightHiddenOutput += wHidOutD;

            // Hidden -> Input
            Matrix wHidOutT = this.weightHiddenOutput.Transposed();

            Matrix hidError = wHidOutT * outError;
            Matrix dHid = Matrix.Map(
                hidden, (el, i, j) => this.activationFunctionInputHidden.Derivate(el)
            );
            Matrix inputT = input.Transposed();

            Matrix hidGrad = Matrix.Hadamard(dHid, hidError) * this.learningRate;
            Matrix wInpHidD = hidGrad * inputT;

            this.biasInputHidden += hidGrad;
            this.weightInputHidden += wInpHidD;
        }
    
        public Matrix Predict(Matrix input) {
            Matrix hidden = this.weightInputHidden * input;
            hidden = hidden + this.biasInputHidden;
            hidden.Map((el, i, j) => this.activationFunctionInputHidden.Activate(el));

            Matrix output = this.weightHiddenOutput * hidden;
            output = output + this.biasHiddenOutput;
            output.Map((el, i, j) => this.activationFunctionHiddenOutput.Activate(el));

            return output;
        }
    
        public void Randomize() {
            this.biasInputHidden.Randomize();
            this.biasHiddenOutput.Randomize();
            this.weightInputHidden.Randomize();
            this.weightHiddenOutput.Randomize();
        }

        public void MutateWeights(Func<float,float> mutationFunction) {
            this.weightInputHidden.Map((el, i, j) => mutationFunction.Invoke(el));
            this.weightHiddenOutput.Map((el, i, j) => mutationFunction.Invoke(el));
        }

        public void MutateBias(Func<float,float> mutationFunction) {
            this.biasInputHidden.Map((el, i, j) => mutationFunction.Invoke(el));
            this.biasHiddenOutput.Map((el, i, j) => mutationFunction.Invoke(el));
        }
    }
}
