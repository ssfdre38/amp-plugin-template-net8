/**
 * AMP Plugin JavaScript Template
 * This script handles the frontend logic for your plugin
 */

(function() {
    'use strict';

    // Plugin name - should match your C# namespace
    const PLUGIN_NAME = '$safeprojectname$';

    /**
     * Call an AMP API method
     * @param {string} method - Method name
     * @param {object} data - Parameters to send
     * @returns {Promise} - Resolves with API response
     */
    async function callAPI(method, data = {}) {
        try {
            const response = await fetch(`/API/${PLUGIN_NAME}/${method}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                credentials: 'include',
                body: JSON.stringify(data)
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const result = await response.json();
            return result;
        } catch (error) {
            console.error('API call failed:', error);
            throw error;
        }
    }

    /**
     * Display result in the result box
     * @param {string} message - Message to display
     * @param {string} type - Type: 'success', 'error', or 'info'
     */
    function displayResult(message, type = 'info') {
        const resultBox = document.getElementById('result');
        if (!resultBox) return;

        resultBox.textContent = message;
        resultBox.className = 'result-box';
        
        if (type === 'success') {
            resultBox.classList.add('success');
        } else if (type === 'error') {
            resultBox.classList.add('error');
        }
    }

    /**
     * Show loading state
     */
    function showLoading() {
        const resultBox = document.getElementById('result');
        if (!resultBox) return;
        
        resultBox.innerHTML = '<div class="loading">Loading</div>';
        resultBox.className = 'result-box';
    }

    /**
     * Initialize the plugin interface
     */
    function init() {
        console.log(`[${PLUGIN_NAME}] Initializing plugin interface`);

        // Example: Test button click handler
        const testButton = document.getElementById('testButton');
        if (testButton) {
            testButton.addEventListener('click', async () => {
                try {
                    showLoading();
                    
                    // Call the example API method
                    const result = await callAPI('ExampleMethod');
                    
                    if (result.success) {
                        displayResult(
                            `Success!\n\n${JSON.stringify(result, null, 2)}`,
                            'success'
                        );
                    } else {
                        displayResult(
                            `Failed: ${result.message || 'Unknown error'}`,
                            'error'
                        );
                    }
                } catch (error) {
                    displayResult(
                        `Error: ${error.message}`,
                        'error'
                    );
                }
            });
        }

        // Add more initialization code here
        // - Set up event listeners
        // - Load initial data
        // - Configure UI elements
    }

    /**
     * Register plugin with AMP's UI system (if available)
     */
    if (window.AMP && window.AMP.registerPlugin) {
        window.AMP.registerPlugin({
            name: PLUGIN_NAME,
            displayName: '$projectname$',
            category: 'Configuration', // Options: Configuration, Management, Tools, etc.
            icon: 'fa-plug', // Font Awesome icon class
            route: '/Plugin.html',
            permission: 'Core.WebPanel.ViewPlugins' // Required permission
        });
    }

    // Initialize when DOM is ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }

})();
