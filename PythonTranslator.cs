using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Python.Runtime;

namespace TeamJRPG
{
    public class PythonTranslator
    {
        private static bool isPythonInitialized = false;

        public static void InitializePythonEngine()
        {
            if (!isPythonInitialized)
            {
                // Get the path to the DLL dynamically
                string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string dllDirectory = Path.GetFullPath(Path.Combine(projectDirectory, @"..\..\..\lib\Python312"));
                string dllPath = Path.Combine(dllDirectory, "python312.dll");

                if (!File.Exists(dllPath))
                {
                    throw new FileNotFoundException($"Python DLL not found at {dllPath}");
                }

                // Set the Python DLL path
                Runtime.PythonDLL = dllPath;

                // Initialize the Python engine
                PythonEngine.Initialize();
                isPythonInitialized = true;
            }
        }

        public static void ShutdownPythonEngine()
        {
            if (isPythonInitialized)
            {
                PythonEngine.Shutdown();
                isPythonInitialized = false;
            }
        }

        public static void RunScript(string scriptName, string methodName, params string[] parameters)
        {
            if (!isPythonInitialized)
            {
                throw new InvalidOperationException("Python engine is not initialized. Call InitializePythonEngine first.");
            }

            // Set the path for the Python script
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string scriptDirectory = Path.GetFullPath(Path.Combine(projectDirectory, @"..\..\..\python"));
            string scriptPath = Path.Combine(scriptDirectory, $"{scriptName}.py");

            if (!File.Exists(scriptPath))
            {
                throw new FileNotFoundException($"Python script not found at {scriptPath}");
            }

            // Execute the Python script
            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                sys.path.append(scriptDirectory);

                // Import the io module to use StringIO
                dynamic io = Py.Import("io");
                // Create a StringIO object to capture output
                dynamic stringIO = io.StringIO();
                // Remember the original stdout and stderr
                dynamic original_stdout = sys.stdout;
                dynamic original_stderr = sys.stderr;

                // Redirect standard output and standard error to the StringIO object
                sys.stdout = stringIO;
                sys.stderr = stringIO;

                try
                {
                    dynamic pythonModule = Py.Import(scriptName);

                    PyObject[] parametersPy = parameters.Select(p => new PyString(p)).ToArray();

                    // Invoke the specified method
                    pythonModule.InvokeMethod(methodName, parametersPy);

                    // Get the captured output
                    string output = stringIO.getvalue().ToString();
                }
                finally
                {
                    // Restore standard output and standard error
                    sys.stdout = original_stdout;
                    sys.stderr = original_stderr;
                }
            }
        }



        public static string ReturnScript(string scriptName, string methodName, params string[] parameters)
        {
            if (!isPythonInitialized)
            {
                throw new InvalidOperationException("Python engine is not initialized. Call InitializePythonEngine first.");
            }

            // Set the path for the Python script
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string scriptDirectory = Path.GetFullPath(Path.Combine(projectDirectory, @"..\..\..\python"));
            string scriptPath = Path.Combine(scriptDirectory, $"{scriptName}.py");

            if (!File.Exists(scriptPath))
            {
                throw new FileNotFoundException($"Python script not found at {scriptPath}");
            }

            // Execute the Python script
            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                sys.path.append(scriptDirectory);

                // Import the io module to use StringIO
                dynamic io = Py.Import("io");
                // Create a StringIO object to capture output
                dynamic stringIO = io.StringIO();
                // Remember the original stdout and stderr
                dynamic original_stdout = sys.stdout;
                dynamic original_stderr = sys.stderr;

                // Redirect standard output and standard error to the StringIO object
                sys.stdout = stringIO;
                sys.stderr = stringIO;

                try
                {
                    dynamic pythonModule = Py.Import(scriptName);

                    PyObject[] parametersPy = parameters.Select(p => new PyString(p)).ToArray();

                    // Invoke the specified method
                    pythonModule.InvokeMethod(methodName, parametersPy);

                    // Get the captured output
                    string output = stringIO.getvalue().ToString();
                    

                    return output;
                }
                finally
                {
                    // Restore standard output and standard error
                    sys.stdout = original_stdout;
                    sys.stderr = original_stderr;
                }
            }
        }
    }
}
