declare module 'react-native-progress-dialog' {
    import { Component } from 'react';
    import { ViewStyle } from 'react-native';

    interface ProgressDialogProps {
        visible: boolean;
        label?: string;
        message?: string;
        onDismiss?: () => void;
        style?: ViewStyle;
    }

    export default class ProgressDialog extends Component<ProgressDialogProps> { }
}
