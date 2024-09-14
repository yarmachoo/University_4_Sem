__kernel void lab2(__global const double *X, __global double* results, __global const double *cycles) {
 
    // Get the index of the current element to be processed
    int id = get_global_id(0);
 
    // Do the operation
    results[id] =  cos(X[0]*(id+1+1024*cycles[0]))/(id+1+1024*cycles[0]);
}

